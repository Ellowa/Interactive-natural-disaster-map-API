using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Validators
{
    public sealed class CreateEventHazardUnitsValidator : AbstractValidator<CreateEventCategoryRequest>
    {
        public CreateEventHazardUnitsValidator()
        {
            RuleFor(c => c.CreateEventCategoryDto.CategoryName).NotEmpty();
        }
    }
}
