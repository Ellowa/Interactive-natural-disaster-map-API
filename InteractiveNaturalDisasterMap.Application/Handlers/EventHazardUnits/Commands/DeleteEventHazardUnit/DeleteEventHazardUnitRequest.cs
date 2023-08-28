using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.DeleteEventHazardUnit
{
    public class DeleteEventHazardUnitRequest : IRequest
    {
        public DeleteEventHazardUnitDto DeleteEventHazardUnitDto { get; set; } = null!;
    }
}
