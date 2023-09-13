using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.AddToEventsCollection
{
    public class AddToEventsCollectionHandler : IRequestHandler<AddToEventsCollectionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventsCollectionRepository _eventsCollectionRepository;

        public AddToEventsCollectionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventsCollectionRepository = unitOfWork.EventsCollectionRepository;
        }

        public async Task Handle(AddToEventsCollectionRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo = await _unitOfWork.EventsCollectionInfoRepository.GetByIdAsync(request.AddToEventsCollectionDto.CollectionId, cancellationToken)
                         ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.AddToEventsCollectionDto.CollectionId);
            if (collectionInfo.UserId != request.UserId)
                throw new AuthorizationException(nameof(collectionInfo), request.UserId);

            var entity = request.AddToEventsCollectionDto.Map();
            await _eventsCollectionRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
