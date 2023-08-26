using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetByIdEventSource
{
    public class GetByIdEventSourceRequest : IRequest<EventSourceDto>
    {
        public GetByIdEventSourceDto GetByIdEventSourceDto { get; set; } = null!;
    }
}
