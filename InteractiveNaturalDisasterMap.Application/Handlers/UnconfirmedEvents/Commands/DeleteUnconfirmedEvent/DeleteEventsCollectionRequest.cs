using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.DeleteUnconfirmedEvent
{
    public class DeleteUnconfirmedEventRequest : IRequest
    {
        public DeleteUnconfirmedEventDto DeleteUnconfirmedEventDto { get; set; } = null!;
    }
}
