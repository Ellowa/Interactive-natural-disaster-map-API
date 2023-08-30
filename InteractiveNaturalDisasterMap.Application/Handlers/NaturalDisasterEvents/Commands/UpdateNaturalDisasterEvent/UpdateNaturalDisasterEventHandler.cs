using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
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
            var unconfirmedEvent = await _unitOfWork.UnconfirmedEventRepository.GetByEventId(request.UpdateNaturalDisasterEventDto.Id, cancellationToken)
                                   ?? throw new NotFoundException(nameof(UnconfirmedEvent), request.UpdateNaturalDisasterEventDto.Id);
            if (unconfirmedEvent.UserId != request.UpdateNaturalDisasterEventDto.UserId)
                throw new AuthorizationException(nameof(unconfirmedEvent), request.UpdateNaturalDisasterEventDto.UserId);

            var eventHazardUnits = (await _unitOfWork.MagnitudeUnitRepository
                                        .GetByIdAsync(request.UpdateNaturalDisasterEventDto.MagnitudeUnitId, cancellationToken, mu => mu.EventHazardUnits) 
                                    ?? throw new NotFoundException(nameof(MagnitudeUnit), request.UpdateNaturalDisasterEventDto.MagnitudeUnitId))
                .EventHazardUnits;
            if (eventHazardUnits == null || eventHazardUnits.Count == 0)
                throw new NotFoundException(nameof(EventHazardUnit), $"With MagnitudeUnitId: {request.UpdateNaturalDisasterEventDto.MagnitudeUnitId}");

            eventHazardUnits = eventHazardUnits.OrderByDescending(ehu => ehu.ThresholdValue).ToArray();
            int eventHazardUnitId = eventHazardUnits.Last().Id;
            if (request.UpdateNaturalDisasterEventDto.MagnitudeValue != null && eventHazardUnits.Count() > 1)
            {
                eventHazardUnitId = eventHazardUnits.First(ehu => ehu.ThresholdValue <= request.UpdateNaturalDisasterEventDto.MagnitudeValue).Id;
            }

            _naturalDisasterEventRepository.Update(request.UpdateNaturalDisasterEventDto.Map(eventHazardUnitId));
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
