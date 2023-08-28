using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.CreateEventHazardUnit
{
    public class CreateEventHazardUnitRequest : IRequest<int>
    {
        public CreateEventHazardUnitDto CreateEventHazardUnitDto { get; set; } = null!;
    }
}
