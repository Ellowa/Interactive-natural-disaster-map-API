using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.DeleteNaturalDisasterEvent
{
    public class DeleteNaturalDisasterEventHandler : IRequestHandler<DeleteNaturalDisasterEventRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public DeleteNaturalDisasterEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _naturalDisasterEventRepository = unitOfWork.NaturalDisasterEventRepository;
        }

        public async Task Handle(DeleteNaturalDisasterEventRequest request, CancellationToken cancellationToken)
        {
            var unconfirmedEvent = await _unitOfWork.UnconfirmedEventRepository.GetByEventId(request.DeleteNaturalDisasterEventDto.Id, cancellationToken) 
                                   ?? throw new NotFoundException(nameof(UnconfirmedEvent), request.DeleteNaturalDisasterEventDto.Id);
            if (unconfirmedEvent.UserId != request.UserId)
                throw new AuthorizationException(nameof(unconfirmedEvent), request.UserId);

            await _naturalDisasterEventRepository.DeleteByIdAsync(request.DeleteNaturalDisasterEventDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
