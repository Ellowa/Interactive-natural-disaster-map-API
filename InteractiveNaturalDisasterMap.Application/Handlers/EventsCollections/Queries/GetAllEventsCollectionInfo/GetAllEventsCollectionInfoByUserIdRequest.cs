using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetAllEventsCollectionInfo
{
    public class GetAllEventsCollectionInfoByUserIdRequest : IRequest<IList<EventsCollectionInfoDto>>
    {
        public GetAllEventsCollectionInfoDto GetAllEventsCollectionInfoDto { get; set; } = null!;

        public int UserId { get; set; }
    }
}
