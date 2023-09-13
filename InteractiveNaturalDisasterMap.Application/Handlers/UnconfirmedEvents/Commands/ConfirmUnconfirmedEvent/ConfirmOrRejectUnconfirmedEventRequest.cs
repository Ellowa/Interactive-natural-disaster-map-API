using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.ConfirmUnconfirmedEvent
{
    public class ConfirmOrRejectUnconfirmedEventRequest : IRequest
    {
        public ConfirmOrRejectUnconfirmedEventDto ConfirmUnconfirmedEventDto { get; set; } = null!;
        public bool? Reject { get; set; }
    }
}
