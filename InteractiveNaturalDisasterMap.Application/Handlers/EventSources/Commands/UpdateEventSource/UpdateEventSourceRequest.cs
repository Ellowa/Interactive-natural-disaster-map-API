using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.UpdateEventSource
{
    public class UpdateEventSourceRequest : IRequest
    {
        public UpdateEventSourceDto UpdateEventSourceDto { get; set; } = null!;
    }
}
