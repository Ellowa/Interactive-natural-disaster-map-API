using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.AddToEventsCollection
{
    public class AddToEventsCollectionHandler : IRequestHandler<AddToEventsCollectionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventsCollectionRepository _eventsCollectionRepository;
        private readonly IAuthorizationService _authorizationService;

        public AddToEventsCollectionHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _eventsCollectionRepository = unitOfWork.EventsCollectionRepository;
        }

        public async Task Handle(AddToEventsCollectionRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo =
                await _unitOfWork.EventsCollectionInfoRepository.GetByIdAsync(request.AddToEventsCollectionDto.CollectionId, cancellationToken)
                ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.AddToEventsCollectionDto.CollectionId);

            await _authorizationService.AuthorizeAsync(request.UserId, collectionInfo.UserId, cancellationToken, collectionInfo, collectionInfo.Id);

            IEnumerable<NaturalDisasterEvent> confirmedNaturalDisasterEvents =
                await _unitOfWork.NaturalDisasterEventRepository.GetAllAsync(cancellationToken, nde => nde.Confirmed);

            IEnumerable<NaturalDisasterEvent> userUnconfirmedEvents = (await _unitOfWork.UnconfirmedEventRepository
                    .GetAllAsync(cancellationToken, ue => ue.UserId == request.UserId))
                .Select(ue => ue.Event);
            var naturalDisasterEvents = confirmedNaturalDisasterEvents.Union(userUnconfirmedEvents);
            _ = naturalDisasterEvents.FirstOrDefault(nde => nde.Id == request.AddToEventsCollectionDto.EventId)
                ?? throw new NotFoundException(nameof(NaturalDisasterEvent), request.AddToEventsCollectionDto.EventId);

            var entity = request.AddToEventsCollectionDto.Map();
            await _eventsCollectionRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
