using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.DeleteNaturalDisasterEvent
{
    public class DeleteNaturalDisasterEventHandler : IRequestHandler<DeleteNaturalDisasterEventRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;
        private readonly IAuthorizationService _authorizationService;

        public DeleteNaturalDisasterEventHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _naturalDisasterEventRepository = unitOfWork.NaturalDisasterEventRepository;
        }

        public async Task Handle(DeleteNaturalDisasterEventRequest request, CancellationToken cancellationToken)
        {
            var naturalDisasterEvent =
                await _naturalDisasterEventRepository.GetByIdAsync(request.DeleteNaturalDisasterEventDto.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(NaturalDisasterEvent), request.DeleteNaturalDisasterEventDto.Id);

            var unconfirmedEvent =
                await _unitOfWork.UnconfirmedEventRepository.GetByEventId(naturalDisasterEvent.Id, cancellationToken);

            if (unconfirmedEvent == null)
            {
                var user =
                    await _unitOfWork.UserRepository.GetByIdAsync(request.UserId, cancellationToken, u => u.Role) 
                    ?? throw new NotFoundException(nameof(User), request.UserId);
                if (user.Role.RoleName != "moderator")
                {
                    throw new NotFoundException(nameof(UnconfirmedEvent), naturalDisasterEvent.Id);
                }
            }
            else
            {
                await _authorizationService.AuthorizeAsync(request.UserId, unconfirmedEvent.UserId, cancellationToken, unconfirmedEvent, unconfirmedEvent.EventId);
            }

            await _naturalDisasterEventRepository.DeleteByIdAsync(naturalDisasterEvent.Id,
                cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
