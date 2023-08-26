using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.CreateEventSource
{
    public class CreateEventSourceRequest : IRequest<int>
    {
        public CreateEventSourceDto CreateEventSourceDto { get; set; } = null!;
    }
}
