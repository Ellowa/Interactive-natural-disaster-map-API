using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetAllEventsCollectionInfo
{
    public class GetAllEventsCollectionInfoByUserIdHandler : IRequestHandler<GetAllEventsCollectionInfoByUserIdRequest, IList<EventsCollectionInfoDto>>
    {
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;

        public GetAllEventsCollectionInfoByUserIdHandler(IUnitOfWork unitOfWork)
        {
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task<IList<EventsCollectionInfoDto>> Handle(GetAllEventsCollectionInfoByUserIdRequest request, CancellationToken cancellationToken)
        {
            if (request.GetAllEventsCollectionInfoDto.UserId != request.CurrentUserId)
                throw new AuthorizationException(nameof(EventsCollectionInfo), request.CurrentUserId);

            Expression<Func<EventsCollectionInfo, bool>> filter = eci => eci.UserId == request.GetAllEventsCollectionInfoDto.UserId;
            var eventsCollectionInfos =
                (await _eventsCollectionInfoRepository.GetAllAsync(cancellationToken, filter,
                    eci => eci.User,
                    eci => eci.EventsCollection.Select(ec => ec.Event.Category),
                    eci => eci.EventsCollection.Select(ec => ec.Event.Source),
                    eci => eci.EventsCollection.Select(ec => ec.Event.MagnitudeUnit),
                    eci => eci.EventsCollection.Select(ec => ec.Event.EventHazardUnit)));
            IList<EventsCollectionInfoDto> eventsCollectionInfoDtos = new List<EventsCollectionInfoDto>(); 
            foreach (var eventsCollectionInfo in eventsCollectionInfos)
            {
                eventsCollectionInfoDtos.Add(new EventsCollectionInfoDto(eventsCollectionInfo));
            }

            return eventsCollectionInfoDtos;
        }
    }
}
