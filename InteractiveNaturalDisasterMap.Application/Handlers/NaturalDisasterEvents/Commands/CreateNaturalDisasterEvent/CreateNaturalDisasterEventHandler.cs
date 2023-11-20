using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent
{
    public class CreateNaturalDisasterEventHandler : IRequestHandler<CreateNaturalDisasterEventRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public CreateNaturalDisasterEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _naturalDisasterEventRepository = unitOfWork.NaturalDisasterEventRepository;
        }

        public async Task<int> Handle(CreateNaturalDisasterEventRequest request, CancellationToken cancellationToken)
        {
            var eventSource = (await _unitOfWork.EventSourceRepository.GetAllAsync(cancellationToken, mu => mu.SourceType == request.SourceName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(EventSource), $"With name {request.SourceName}");
            var magnitudeUnit = (await _unitOfWork.MagnitudeUnitRepository.GetAllAsync(cancellationToken,
                    mu => mu.MagnitudeUnitName == request.CreateNaturalDisasterEventDto.MagnitudeUnitName,
                    mu => mu.EventHazardUnits, mu => mu.EventCategories))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(MagnitudeUnit),
                    $"With name {request.CreateNaturalDisasterEventDto.MagnitudeUnitName}");
            var eventHazardUnits = magnitudeUnit.EventHazardUnits;
            if (eventHazardUnits == null || eventHazardUnits.Count == 0)
                throw new NotFoundException(nameof(EventHazardUnit),
                    $"With MagnitudeUnitName: {request.CreateNaturalDisasterEventDto.MagnitudeUnitName}");

            var eventCategory = (await _unitOfWork.EventCategoryRepository.GetAllAsync(cancellationToken,
                    mu => mu.CategoryName == request.CreateNaturalDisasterEventDto.EventCategoryName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(EventCategory),
                    $"With name {request.CreateNaturalDisasterEventDto.EventCategoryName}");
            if (!magnitudeUnit.EventCategories.Contains(eventCategory))
                throw new NotFoundException(nameof(EventCategory),
                    $"With name {eventCategory.CategoryName} in this {magnitudeUnit.MagnitudeUnitName} magnitudeUnit");

            eventHazardUnits = eventHazardUnits.OrderByDescending(ehu => ehu.ThresholdValue).ToArray();
            int eventHazardUnitId = eventHazardUnits.Last().Id;
            if (request.CreateNaturalDisasterEventDto.MagnitudeValue != null && eventHazardUnits.Count > 1)
            {
                eventHazardUnitId = eventHazardUnits.First(ehu => ehu.ThresholdValue <= request.CreateNaturalDisasterEventDto.MagnitudeValue).Id;
            }

            bool isConfirmedEvent = eventSource.SourceType != "user" && eventSource.SourceType != "unknown";

            var naturalDisasterEventEntity = request.CreateNaturalDisasterEventDto.Map(isConfirmedEvent, eventHazardUnitId, eventSource.Id,
                eventCategory.Id, magnitudeUnit.Id, request.IdInThirdPartyApi);
            await _naturalDisasterEventRepository.AddAsync(naturalDisasterEventEntity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            if (!isConfirmedEvent)
            {
                var unconfirmedEvent = new UnconfirmedEvent()
                {
                    EventId = naturalDisasterEventEntity.Id,
                    UserId = (int)request.UserId!,
                    IsChecked = false,
                };
                await _unitOfWork.UnconfirmedEventRepository.AddAsync(unconfirmedEvent, cancellationToken);

                var userEventsCollection =
                    (await _unitOfWork.EventsCollectionInfoRepository.GetAllAsync(cancellationToken,
                        mu => (mu.CollectionName == "your Events") && mu.UserId == (int)request.UserId)).FirstOrDefault();
                if (userEventsCollection == null)
                {
                    userEventsCollection = new EventsCollectionInfo
                    {
                        CollectionName = "your Events",
                        UserId = (int)request.UserId!,
                    };
                    await _unitOfWork.EventsCollectionInfoRepository.AddAsync(userEventsCollection, cancellationToken);
                    await _unitOfWork.SaveAsync(cancellationToken);
                }

                await _unitOfWork.EventsCollectionRepository.AddAsync(
                    new EventsCollection
                    {
                        EventId = naturalDisasterEventEntity.Id,
                        CollectionId = userEventsCollection.Id,
                    },
                    cancellationToken);

                await _unitOfWork.SaveAsync(cancellationToken);
            }

            return naturalDisasterEventEntity.Id;
        }
    }
}
