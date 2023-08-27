using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.UpdateMagnitudeUnit
{
    public class UpdateMagnitudeUnitRequest : IRequest
    {
        public UpdateMagnitudeUnitDto UpdateMagnitudeUnitDto { get; set; } = null!;
    }
}
