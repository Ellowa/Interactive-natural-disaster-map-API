﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.UpdateNaturalDisasterEvent
{
    public class UpdateNaturalDisasterEventHandler : IRequestHandler<UpdateNaturalDisasterEventRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public UpdateNaturalDisasterEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _naturalDisasterEventRepository = unitOfWork.NaturalDisasterEventRepository;
        }

        public async Task Handle(UpdateNaturalDisasterEventRequest request, CancellationToken cancellationToken)
        {
            var naturalDisasterEvent =
                await _naturalDisasterEventRepository.GetByIdAsync(request.UpdateNaturalDisasterEventDto.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(NaturalDisasterEvent), request.UpdateNaturalDisasterEventDto.Id);

            var unconfirmedEvent =
                await _unitOfWork.UnconfirmedEventRepository.GetByEventId(request.UpdateNaturalDisasterEventDto.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(UnconfirmedEvent), request.UpdateNaturalDisasterEventDto.Id);
            if (unconfirmedEvent.UserId != request.UserId)
                throw new AuthorizationException(nameof(unconfirmedEvent), request.UserId);

            var eventCategory = (await _unitOfWork.EventCategoryRepository.GetAllAsync(cancellationToken, mu => mu.CategoryName == request.UpdateNaturalDisasterEventDto.EventCategoryName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(EventCategory), $"With name {request.UpdateNaturalDisasterEventDto.EventCategoryName}");

            var magnitudeUnit = (await _unitOfWork.MagnitudeUnitRepository.GetAllAsync(cancellationToken, mu => mu.MagnitudeUnitName == request.UpdateNaturalDisasterEventDto.MagnitudeUnitName,
                    mu => mu.EventHazardUnits))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(MagnitudeUnit), $"With name {request.UpdateNaturalDisasterEventDto.MagnitudeUnitName}");
            var eventHazardUnits = magnitudeUnit.EventHazardUnits;
            if (eventHazardUnits == null || eventHazardUnits.Count == 0)
                throw new NotFoundException(nameof(EventHazardUnit),
                    $"With MagnitudeUnitName: {request.UpdateNaturalDisasterEventDto.MagnitudeUnitName}");

            eventHazardUnits = eventHazardUnits.OrderByDescending(ehu => ehu.ThresholdValue).ToArray();
            int eventHazardUnitId = eventHazardUnits.Last().Id;
            if (request.UpdateNaturalDisasterEventDto.MagnitudeValue != null && eventHazardUnits.Count() > 1)
            {
                eventHazardUnitId = eventHazardUnits.First(ehu =>
                    ehu.ThresholdValue <= request.UpdateNaturalDisasterEventDto.MagnitudeValue).Id;
            }

            naturalDisasterEvent.Id = request.UpdateNaturalDisasterEventDto.Id;
            naturalDisasterEvent.Title = request.UpdateNaturalDisasterEventDto.Title;
            naturalDisasterEvent.Link = request.UpdateNaturalDisasterEventDto.Link;
            naturalDisasterEvent.StartDate = request.UpdateNaturalDisasterEventDto.StartDate;
            naturalDisasterEvent.EndDate = request.UpdateNaturalDisasterEventDto.EndDate;
            naturalDisasterEvent.MagnitudeValue = request.UpdateNaturalDisasterEventDto.MagnitudeValue;
            naturalDisasterEvent.EventCategoryId = eventCategory.Id;
            naturalDisasterEvent.MagnitudeUnitId = magnitudeUnit.Id;
            naturalDisasterEvent.EventHazardUnitId = eventHazardUnitId;
            naturalDisasterEvent.Latitude = request.UpdateNaturalDisasterEventDto.Latitude;
            naturalDisasterEvent.Longitude = request.UpdateNaturalDisasterEventDto.Longitude;

            _naturalDisasterEventRepository.Update(naturalDisasterEvent);
            unconfirmedEvent.IsChecked = false;
            _unitOfWork.UnconfirmedEventRepository.Update(unconfirmedEvent);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
