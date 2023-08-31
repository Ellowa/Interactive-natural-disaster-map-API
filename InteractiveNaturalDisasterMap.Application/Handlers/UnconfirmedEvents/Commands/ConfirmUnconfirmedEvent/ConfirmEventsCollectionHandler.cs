using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.ConfirmUnconfirmedEvent
{
    public class ConfirmUnconfirmedEventHandler : IRequestHandler<ConfirmUnconfirmedEventRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnconfirmedEventRepository _unconfirmedEventRepository;

        public ConfirmUnconfirmedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unconfirmedEventRepository = unitOfWork.UnconfirmedEventRepository;
        }

        public async Task Handle(ConfirmUnconfirmedEventRequest request, CancellationToken cancellationToken)
        {
            var unconfirmedEvent =
                (await _unconfirmedEventRepository.GetByEventId(request.ConfirmUnconfirmedEventDto.EventId, cancellationToken, ue => ue.Event))
                ?? throw new NotFoundException(nameof(UnconfirmedEvent), request.ConfirmUnconfirmedEventDto.EventId);

            if (request.ConfirmUnconfirmedEventDto.Reject == null || request.ConfirmUnconfirmedEventDto.Reject == false)
            {
                unconfirmedEvent.Event.Confirmed = true;
                _unitOfWork.NaturalDisasterEventRepository.Update(unconfirmedEvent.Event);
                _unconfirmedEventRepository.Delete(unconfirmedEvent);
            }
            else
            {
                unconfirmedEvent.IsChecked = true;
                _unconfirmedEventRepository.Update(unconfirmedEvent);
            }
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
