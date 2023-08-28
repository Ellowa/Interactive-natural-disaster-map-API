using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetByIdEventHazardUnit
{
    public class GetByIdEventHazardUnitRequest : IRequest<EventHazardUnitDto>
    {
        public GetByIdEventHazardUnitDto GetByIdEventHazardUnitDto { get; set; } = null!;
    }
}
