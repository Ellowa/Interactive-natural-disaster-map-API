using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetAllMagnitudeUnit
{
    public class GetAllMagnitudeUnitRequest : IRequest<IList<MagnitudeUnitDto>>
    {
    }
}
