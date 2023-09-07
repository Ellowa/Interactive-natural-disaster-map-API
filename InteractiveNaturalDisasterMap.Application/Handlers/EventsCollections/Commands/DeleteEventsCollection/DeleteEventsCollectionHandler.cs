using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteEventsCollection
{
    public class DeleteEventsCollectionHandler : IRequestHandler<DeleteEventsCollectionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventsCollectionRepository _eventsCollectionRepository;

        public DeleteEventsCollectionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventsCollectionRepository = unitOfWork.EventsCollectionRepository;
        }

        public async Task Handle(DeleteEventsCollectionRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo = await _unitOfWork.EventsCollectionInfoRepository.GetByIdAsync(request.DeleteEventsCollectionDto.CollectionId, cancellationToken)
                                 ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.DeleteEventsCollectionDto.CollectionId);
            if (collectionInfo.UserId != request.UserId)
                throw new AuthorizationException(nameof(collectionInfo), request.UserId);

            var entity = request.DeleteEventsCollectionDto.Map();
            _eventsCollectionRepository.Delete(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
