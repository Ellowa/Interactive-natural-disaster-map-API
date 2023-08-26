using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.DeleteEventSource
{
    public class DeleteEventSourceRequest : IRequest
    {
        public DeleteEventSourceDto DeleteEventSourceDto { get; set; } = null!;
    }
}
