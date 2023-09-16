using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Validators
{
    public sealed class UpdateNaturalDisasterEventValidator : AbstractValidator<UpdateNaturalDisasterEventDto>
    {
        public UpdateNaturalDisasterEventValidator()
        {
            RuleFor(u => u.Title).NotEmpty();
            RuleFor(u => u.StartDate).LessThanOrEqualTo(DateTime.Now.ToUniversalTime());
            RuleFor(u => u.EndDate)
                .GreaterThan(u => u.StartDate)
                .LessThanOrEqualTo(DateTime.Now.ToUniversalTime()).When(u => u.EndDate != null);
            RuleFor(u => u.MagnitudeValue)
                .GreaterThan(0)
                .When(u => u.MagnitudeValue != null);
            RuleFor(u => u.EventCategoryName).NotEmpty();
            RuleFor(u => u.MagnitudeUnitName).NotEmpty();
            RuleFor(u => u.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(u => u.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
