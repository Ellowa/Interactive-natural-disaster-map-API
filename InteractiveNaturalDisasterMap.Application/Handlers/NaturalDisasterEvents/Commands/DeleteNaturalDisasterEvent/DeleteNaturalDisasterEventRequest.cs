using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.DeleteNaturalDisasterEvent
{
    public class DeleteNaturalDisasterEventRequest : IRequest
    {
        public DeleteNaturalDisasterEventDto DeleteNaturalDisasterEventDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
