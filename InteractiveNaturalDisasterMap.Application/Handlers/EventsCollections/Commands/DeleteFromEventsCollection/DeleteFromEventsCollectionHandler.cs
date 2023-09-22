using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteFromEventsCollection
{
    public class DeleteFromEventsCollectionHandler : IRequestHandler<DeleteFromEventsCollectionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventsCollectionRepository _eventsCollectionRepository;
        private readonly IAuthorizationService _authorizationService;

        public DeleteFromEventsCollectionHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _eventsCollectionRepository = unitOfWork.EventsCollectionRepository;
        }

        public async Task Handle(DeleteFromEventsCollectionRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo = await _unitOfWork.EventsCollectionInfoRepository.GetByIdAsync(request.DeleteFromEventsCollectionDto.CollectionId, cancellationToken)
                                 ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.DeleteFromEventsCollectionDto.CollectionId);

            await _authorizationService.AuthorizeAsync(request.UserId, collectionInfo.UserId, cancellationToken, collectionInfo);

            var entity = request.DeleteFromEventsCollectionDto.Map();
            _eventsCollectionRepository.Delete(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
