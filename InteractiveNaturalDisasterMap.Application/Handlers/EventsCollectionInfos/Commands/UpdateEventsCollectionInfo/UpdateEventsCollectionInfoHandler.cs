using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.UpdateEventsCollectionInfo
{
    public class UpdateEventsCollectionInfoHandler : IRequestHandler<UpdateEventsCollectionInfoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;
        private readonly IAuthorizationService _authorizationService;

        public UpdateEventsCollectionInfoHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task Handle(UpdateEventsCollectionInfoRequest request, CancellationToken cancellationToken)
        {
            var collectionInfo = await _eventsCollectionInfoRepository.GetByIdAsync(request.UpdateEventsCollectionInfoDto.Id, cancellationToken) 
                                 ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.UpdateEventsCollectionInfoDto.Id);

            await _authorizationService.AuthorizeAsync(request.UserId, collectionInfo.UserId, cancellationToken, collectionInfo);

            collectionInfo.CollectionName = request.UpdateEventsCollectionInfoDto.CollectionName;
            _eventsCollectionInfoRepository.Update(collectionInfo);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
