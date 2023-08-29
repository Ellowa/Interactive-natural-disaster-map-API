using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.CreateUnconfirmedEvent
{
    public class CreateUnconfirmedEventRequest : IRequest
    {
        public CreateUnconfirmedEventDto CreateUnconfirmedEventDto { get; set; } = null!;
    }
}
