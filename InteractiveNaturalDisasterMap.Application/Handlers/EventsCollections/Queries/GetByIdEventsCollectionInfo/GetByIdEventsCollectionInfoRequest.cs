using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetByIdEventsCollectionInfo
{
    public class GetByIdEventsCollectionInfoRequest : IRequest<EventsCollectionInfoDto>
    {
        public GetByIdEventsCollectionInfoDto GetByIdEventsCollectionInfoDto { get; set; } = null!;
    }
}
