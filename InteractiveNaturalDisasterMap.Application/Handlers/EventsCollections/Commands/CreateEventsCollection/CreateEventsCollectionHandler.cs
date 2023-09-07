using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.CreateEventsCollection
{
    public class CreateEventsCollectionHandler : IRequestHandler<CreateEventsCollectionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventsCollectionRepository _eventsCollectionRepository;

        public CreateEventsCollectionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventsCollectionRepository = unitOfWork.EventsCollectionRepository;
        }

        public async Task Handle(CreateEventsCollectionRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo = await _unitOfWork.EventsCollectionInfoRepository.GetByIdAsync(request.CreateEventsCollectionDto.CollectionId, cancellationToken)
                         ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.CreateEventsCollectionDto.CollectionId);
            if (collectionInfo.UserId != request.UserId)
                throw new AuthorizationException(nameof(collectionInfo), request.UserId);

            var entity = request.CreateEventsCollectionDto.Map();
            await _eventsCollectionRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
