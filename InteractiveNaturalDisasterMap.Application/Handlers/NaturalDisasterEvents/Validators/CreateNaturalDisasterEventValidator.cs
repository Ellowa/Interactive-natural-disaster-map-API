using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Validators
{
    public sealed class CreateNaturalDisasterEventValidator : AbstractValidator<CreateNaturalDisasterEventDto>
    {
        public CreateNaturalDisasterEventValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.StartDate).LessThanOrEqualTo(DateTime.Now.ToUniversalTime());
            RuleFor(c => c.EndDate)
                .GreaterThan(c => c.StartDate)
                .LessThanOrEqualTo(DateTime.Now.ToUniversalTime()).When(c => c.EndDate != null);
            RuleFor(c => c.MagnitudeValue)
                .GreaterThan(0)
                .When(c => c.MagnitudeValue != null);
            RuleFor(c => c.EventCategoryName).NotEmpty();
            RuleFor(c => c.MagnitudeUnitName).NotEmpty();
            RuleFor(c => c.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(c => c.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
