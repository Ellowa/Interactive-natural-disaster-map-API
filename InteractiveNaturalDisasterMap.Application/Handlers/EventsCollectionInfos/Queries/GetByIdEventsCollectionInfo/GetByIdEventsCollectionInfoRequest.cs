
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetByIdEventsCollectionInfo
{
    public class GetByIdEventsCollectionInfoRequest : IRequest<EventsCollectionInfoDto>
    {
        public GetByIdEventsCollectionInfoDto GetByIdEventsCollectionInfoDto { get; set; } = null!;
    }
}
