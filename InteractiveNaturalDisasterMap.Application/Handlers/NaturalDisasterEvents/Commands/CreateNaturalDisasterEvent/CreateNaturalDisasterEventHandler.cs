﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
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
            var eventCategory = (await _unitOfWork.EventCategoryRepository.GetAllAsync(cancellationToken, mu => mu.CategoryName == request.CreateNaturalDisasterEventDto.EventCategoryName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(EventCategory), $"With name {request.CreateNaturalDisasterEventDto.EventCategoryName}");
            var eventSource = (await _unitOfWork.EventSourceRepository.GetAllAsync(cancellationToken, mu => mu.SourceType == request.SourceName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(EventSource), $"With name {request.SourceName}");
            var magnitudeUnit = (await _unitOfWork.MagnitudeUnitRepository.GetAllAsync(cancellationToken, mu => mu.MagnitudeUnitName == request.CreateNaturalDisasterEventDto.MagnitudeUnitName, 
                    mu => mu.EventHazardUnits))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(MagnitudeUnit), $"With name {request.CreateNaturalDisasterEventDto.MagnitudeUnitName}");
            var eventHazardUnits = magnitudeUnit.EventHazardUnits;
            if(eventHazardUnits == null || eventHazardUnits.Count == 0)
                throw new NotFoundException(nameof(EventHazardUnit), $"With MagnitudeUnitName: {request.CreateNaturalDisasterEventDto.MagnitudeUnitName}");

            eventHazardUnits = eventHazardUnits.OrderByDescending(ehu => ehu.ThresholdValue).ToArray();
            int eventHazardUnitId = eventHazardUnits.Last().Id;
            if (request.CreateNaturalDisasterEventDto.MagnitudeValue != null && eventHazardUnits.Count() > 1)
            {
                eventHazardUnitId = eventHazardUnits.First(ehu => ehu.ThresholdValue <= request.CreateNaturalDisasterEventDto.MagnitudeValue).Id;
            }

            bool isConfirmedEvent = eventSource.SourceType != "user";

            var entity = request.CreateNaturalDisasterEventDto.Map(isConfirmedEvent, eventHazardUnitId, eventSource.Id, eventCategory.Id, magnitudeUnit.Id);
            await _naturalDisasterEventRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            if (!isConfirmedEvent)
            {
                var unconfirmedEvent = new UnconfirmedEvent()
                {
                    EventId = entity.Id,
                    UserId = (int)request.UserId!,
                    IsChecked = false,
                };
                await _unitOfWork.UnconfirmedEventRepository.AddAsync(unconfirmedEvent, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);
            }
            return entity.Id;
        }
    }
}
