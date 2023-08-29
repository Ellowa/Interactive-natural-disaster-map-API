using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.DeleteUnconfirmedEvent
{
    public class DeleteUnconfirmedEventHandler : IRequestHandler<DeleteUnconfirmedEventRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnconfirmedEventRepository _unconfirmedEventRepository;

        public DeleteUnconfirmedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unconfirmedEventRepository = unitOfWork.UnconfirmedEventRepository;
        }

        public async Task Handle(DeleteUnconfirmedEventRequest request, CancellationToken cancellationToken)
        {
            var unconfirmedEvent =
                (await _unconfirmedEventRepository.GetByEventId(request.DeleteUnconfirmedEventDto.EventId, cancellationToken, ue => ue.Event))
                ?? throw new NotFoundException(nameof(UnconfirmedEvent), request.DeleteUnconfirmedEventDto.EventId);

            unconfirmedEvent.Event.Confirmed = true;
            _unitOfWork.NaturalDisasterEventRepository.Update(unconfirmedEvent.Event);
            _unconfirmedEventRepository.Delete(unconfirmedEvent);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
