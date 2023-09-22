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
            var unconfirmedEvent = await _unitOfWork.UnconfirmedEventRepository.GetByEventId(request.DeleteNaturalDisasterEventDto.Id, cancellationToken) 
                                   ?? throw new NotFoundException(nameof(UnconfirmedEvent), request.DeleteNaturalDisasterEventDto.Id);

            await _authorizationService.AuthorizeAsync(request.UserId, unconfirmedEvent.UserId, cancellationToken, unconfirmedEvent);

            await _naturalDisasterEventRepository.DeleteByIdAsync(request.DeleteNaturalDisasterEventDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
