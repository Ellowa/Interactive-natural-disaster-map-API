using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetAllEventHazardUnit
{
    public class GetAllEventHazardUnitRequest : IRequest<IList<EventHazardUnitDto>>
    {
    }
}
