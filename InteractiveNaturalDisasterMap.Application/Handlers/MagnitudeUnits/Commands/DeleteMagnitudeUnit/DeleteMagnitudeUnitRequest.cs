using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnit
{
    public class DeleteMagnitudeUnitRequest : IRequest
    {
        public DeleteMagnitudeUnitDto DeleteMagnitudeUnitDto { get; set; } = null!;
    }
}
