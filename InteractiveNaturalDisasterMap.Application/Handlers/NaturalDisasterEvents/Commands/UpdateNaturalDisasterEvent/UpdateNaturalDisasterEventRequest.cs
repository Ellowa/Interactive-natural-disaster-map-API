using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.UpdateNaturalDisasterEvent
{
    public class UpdateNaturalDisasterEventRequest : IRequest
    {
        public UpdateNaturalDisasterEventDto UpdateNaturalDisasterEventDto { get; set; } = null!;
    }
}
