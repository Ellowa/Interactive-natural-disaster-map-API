using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetAllEventsCollectionInfo
{
    public class GetAllEventsCollectionInfoRequest : IRequest<IList<EventsCollectionInfoDto>>
    {
    }
}
