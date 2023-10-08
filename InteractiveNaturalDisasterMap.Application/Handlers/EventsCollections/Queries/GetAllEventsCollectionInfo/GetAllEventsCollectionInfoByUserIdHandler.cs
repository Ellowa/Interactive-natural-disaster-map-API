using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;
using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetAllEventsCollectionInfo
{
    public class GetAllEventsCollectionInfoByUserIdHandler : IRequestHandler<GetAllEventsCollectionInfoByUserIdRequest, IList<EventsCollectionInfoDto>>
    {
        private readonly IEventsCollectionInfoRepository _eventsCollectionInfoRepository;
        private readonly IAuthorizationService _authorizationService;

        public GetAllEventsCollectionInfoByUserIdHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task<IList<EventsCollectionInfoDto>> Handle(GetAllEventsCollectionInfoByUserIdRequest request, CancellationToken cancellationToken)
        {
            request.GetAllEventsCollectionInfoDto.UserId ??= request.UserId;

            await _authorizationService.AuthorizeAsync(request.UserId, (int)request.GetAllEventsCollectionInfoDto.UserId, cancellationToken, null, null);

            Expression<Func<EventsCollectionInfo, bool>> filter = eci => eci.UserId == request.GetAllEventsCollectionInfoDto.UserId;
            var eventsCollection = (await _eventsCollectionInfoRepository.GetAllAsync(cancellationToken, filter)).OrderBy(eci => eci.Id);
            IList<EventsCollectionInfoDto> eventsCollectionInfoDtos = new List<EventsCollectionInfoDto>(); 
            foreach (var eventsCollectionInfo in eventsCollection)
            {
                eventsCollectionInfoDtos.Add(new EventsCollectionInfoDto(eventsCollectionInfo));
            }

            return eventsCollectionInfoDtos;
        }
    }
}
