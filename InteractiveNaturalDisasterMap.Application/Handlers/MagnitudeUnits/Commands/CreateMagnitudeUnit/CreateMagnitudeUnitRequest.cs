using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.CreateMagnitudeUnit
{
    public class CreateMagnitudeUnitRequest : IRequest<int>
    {
        public CreateMagnitudeUnitDto CreateMagnitudeUnitDto { get; set; } = null!;
    }
}
