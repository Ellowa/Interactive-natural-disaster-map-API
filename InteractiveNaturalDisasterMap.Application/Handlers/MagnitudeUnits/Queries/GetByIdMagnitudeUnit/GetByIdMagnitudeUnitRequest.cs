using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetByIdMagnitudeUnit
{
    public class GetByIdMagnitudeUnitRequest : IRequest<MagnitudeUnitDto>
    {
        public GetByIdMagnitudeUnitDto GetByIdMagnitudeUnitDto { get; set; } = null!;
    }
}
