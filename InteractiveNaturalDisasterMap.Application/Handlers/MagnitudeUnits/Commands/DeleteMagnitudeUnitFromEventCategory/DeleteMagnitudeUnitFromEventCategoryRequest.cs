using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnitFromEventCategory
{
    public class DeleteMagnitudeUnitFromEventCategoryRequest : IRequest
    {
        public MagnitudeUnitToEventCategoryDto DeleteMagnitudeUnitFromEventCategoryDto { get; set; } = null!;
    }
}
