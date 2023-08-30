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
            if (await _unitOfWork.EventCategoryRepository.GetByIdAsync(request.CreateNaturalDisasterEventDto.EventCategoryId, cancellationToken) == null)
                throw new NotFoundException(nameof(EventCategory), request.CreateNaturalDisasterEventDto.EventCategoryId);
            var eventSource = await _unitOfWork.EventSourceRepository.GetByIdAsync(request.CreateNaturalDisasterEventDto.SourceId, cancellationToken)
                              ?? throw new NotFoundException(nameof(EventSource), request.CreateNaturalDisasterEventDto.SourceId);
            var eventHazardUnits = (await _unitOfWork.MagnitudeUnitRepository
                                        .GetByIdAsync(request.CreateNaturalDisasterEventDto.EventCategoryId, cancellationToken, mu => mu.EventHazardUnits)
                                    ?? throw new NotFoundException(nameof(MagnitudeUnit), request.CreateNaturalDisasterEventDto.MagnitudeUnitId))
                                    .EventHazardUnits;
            if(eventHazardUnits == null || eventHazardUnits.Count == 0)
                throw new NotFoundException(nameof(EventHazardUnit), $"With MagnitudeUnitId: {request.CreateNaturalDisasterEventDto.MagnitudeUnitId}");

            eventHazardUnits = eventHazardUnits.OrderByDescending(ehu => ehu.ThresholdValue).ToArray();
            int eventHazardUnitId = eventHazardUnits.Last().Id;
            if (request.CreateNaturalDisasterEventDto.MagnitudeValue != null && eventHazardUnits.Count() > 1)
            {
                eventHazardUnitId = eventHazardUnits.First(ehu => ehu.ThresholdValue <= request.CreateNaturalDisasterEventDto.MagnitudeValue).Id;
            }

            var entity = request.CreateNaturalDisasterEventDto.Map(eventSource.SourceType!="User", eventHazardUnitId);
            await _naturalDisasterEventRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
