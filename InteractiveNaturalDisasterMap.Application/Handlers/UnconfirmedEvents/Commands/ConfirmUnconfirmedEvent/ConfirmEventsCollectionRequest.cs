using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.ConfirmUnconfirmedEvent
{
    public class ConfirmUnconfirmedEventRequest : IRequest
    {
        public ConfirmUnconfirmedEventDto ConfirmUnconfirmedEventDto { get; set; } = null!;
    }
}
