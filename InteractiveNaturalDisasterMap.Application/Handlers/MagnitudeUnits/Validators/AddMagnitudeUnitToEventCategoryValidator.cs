using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.AddMagnitudeUnitToEventCategory;
namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Validators
{
    public sealed class AddMagnitudeUnitToEventCategoryValidator : AbstractValidator<AddMagnitudeUnitToEventCategoryRequest>
    {
        public AddMagnitudeUnitToEventCategoryValidator()
        {
            RuleFor(c => c.AddMagnitudeUnitToEventCategoryDto.MagnitudeUnitName).NotEmpty();
            RuleFor(c => c.AddMagnitudeUnitToEventCategoryDto.EventCategoryName).NotEmpty();
        }
    }
}
