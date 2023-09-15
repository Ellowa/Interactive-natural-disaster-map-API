using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetAllEventsCollectionInfo
{
    public class GetAllEventsCollectionInfoByUserIdRequest : IRequest<IList<EventsCollectionInfoDto>>
    {
        public GetAllEventsCollectionInfoDto GetAllEventsCollectionInfoDto { get; set; } = null!;

        public int UserId { get; set; }
    }
}
