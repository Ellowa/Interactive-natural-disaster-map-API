using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnitFromEventCategory;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Validators
{
    public sealed class DeleteMagnitudeUnitFromEventCategoryValidator : AbstractValidator<DeleteMagnitudeUnitFromEventCategoryRequest>
    {
        public DeleteMagnitudeUnitFromEventCategoryValidator()
        {
            RuleFor(c => c.DeleteMagnitudeUnitFromEventCategoryDto.MagnitudeUnitName).NotEmpty();
            RuleFor(c => c.DeleteMagnitudeUnitFromEventCategoryDto.EventCategoryName).NotEmpty();
        }
    }
}
