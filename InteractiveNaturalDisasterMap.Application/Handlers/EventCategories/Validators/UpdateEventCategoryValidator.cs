using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.UpdateEventCategory;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Validators
{
    public sealed class UpdateEventCategoryValidator : AbstractValidator<UpdateEventCategoryRequest>
    {
        public UpdateEventCategoryValidator()
        {
            RuleFor(c => c.UpdateEventCategoryDto.Id).NotNull();
            RuleFor(c => c.UpdateEventCategoryDto.CategoryName).NotEmpty();
        }
    }
}
