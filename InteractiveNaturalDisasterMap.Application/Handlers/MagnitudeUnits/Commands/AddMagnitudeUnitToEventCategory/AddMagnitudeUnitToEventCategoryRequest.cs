using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.AddMagnitudeUnitToEventCategory
{
    public class AddMagnitudeUnitToEventCategoryRequest : IRequest
    {
        public MagnitudeUnitToEventCategoryDto AddMagnitudeUnitToEventCategoryDto { get; set; } = null!;
    }
}
