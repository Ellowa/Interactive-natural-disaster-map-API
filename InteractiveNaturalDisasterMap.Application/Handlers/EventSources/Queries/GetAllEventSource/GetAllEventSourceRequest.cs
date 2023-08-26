using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetAllEventSource
{
    public class GetAllEventSourceRequest : IRequest<IList<EventSourceDto>>
    {
    }
}
