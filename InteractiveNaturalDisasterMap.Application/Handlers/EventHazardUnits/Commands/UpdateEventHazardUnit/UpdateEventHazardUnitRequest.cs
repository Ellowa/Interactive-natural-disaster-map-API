using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.UpdateEventHazardUnit
{
    public class UpdateEventHazardUnitRequest : IRequest
    {
        public UpdateEventHazardUnitDto UpdateEventHazardUnitDto { get; set; } = null!;
    }
}
