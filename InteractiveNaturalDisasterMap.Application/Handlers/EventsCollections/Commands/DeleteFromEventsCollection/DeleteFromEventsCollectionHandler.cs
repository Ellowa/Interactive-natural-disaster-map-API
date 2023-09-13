using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteFromEventsCollection
{
    public class DeleteFromEventsCollectionHandler : IRequestHandler<DeleteFromEventsCollectionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventsCollectionRepository _eventsCollectionRepository;

        public DeleteFromEventsCollectionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventsCollectionRepository = unitOfWork.EventsCollectionRepository;
        }

        public async Task Handle(DeleteFromEventsCollectionRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo = await _unitOfWork.EventsCollectionInfoRepository.GetByIdAsync(request.DeleteFromEventsCollectionDto.CollectionId, cancellationToken)
                                 ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.DeleteFromEventsCollectionDto.CollectionId);
            if (collectionInfo.UserId != request.UserId)
                throw new AuthorizationException(nameof(collectionInfo), request.UserId);

            var entity = request.DeleteFromEventsCollectionDto.Map();
            _eventsCollectionRepository.Delete(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
