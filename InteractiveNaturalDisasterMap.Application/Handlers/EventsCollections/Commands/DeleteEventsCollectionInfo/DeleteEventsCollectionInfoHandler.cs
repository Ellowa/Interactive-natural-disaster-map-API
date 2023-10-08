using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteEventsCollectionInfo
{
    public class DeleteEventsCollectionInfoHandler : IRequestHandler<DeleteEventsCollectionInfoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;
        private readonly IAuthorizationService _authorizationService;

        public DeleteEventsCollectionInfoHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task Handle(DeleteEventsCollectionInfoRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo = await _eventsCollectionInfoRepository.GetByIdAsync(request.DeleteEventsCollectionInfoDto.Id, cancellationToken)
                                 ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.DeleteEventsCollectionInfoDto.Id);

            await _authorizationService.AuthorizeAsync(request.UserId, collectionInfo.UserId, cancellationToken, collectionInfo, collectionInfo.Id);

            await _eventsCollectionInfoRepository.DeleteByIdAsync(request.DeleteEventsCollectionInfoDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
