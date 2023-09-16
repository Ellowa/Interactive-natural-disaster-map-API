using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Validators
{
    public sealed class CreateNaturalDisasterEventValidator : AbstractValidator<CreateNaturalDisasterEventRequest>
    {
        public CreateNaturalDisasterEventValidator()
        {
            RuleFor(c => c.CreateNaturalDisasterEventDto.Title).NotEmpty();
            RuleFor(c => c.CreateNaturalDisasterEventDto.StartDate).LessThanOrEqualTo(DateTime.Now.ToUniversalTime());
            RuleFor(c => c.CreateNaturalDisasterEventDto.EndDate)
                .GreaterThan(c => c.CreateNaturalDisasterEventDto.StartDate)
                .LessThanOrEqualTo(DateTime.Now.ToUniversalTime()).When(c => c.CreateNaturalDisasterEventDto.EndDate != null);
            RuleFor(c => c.CreateNaturalDisasterEventDto.MagnitudeValue)
                .GreaterThan(0)
                .When(c => c.CreateNaturalDisasterEventDto.MagnitudeValue != null);
            RuleFor(c => c.CreateNaturalDisasterEventDto.EventCategoryName).NotEmpty();
            RuleFor(c => c.CreateNaturalDisasterEventDto.MagnitudeUnitName).NotEmpty();
            RuleFor(c => c.CreateNaturalDisasterEventDto.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(c => c.CreateNaturalDisasterEventDto.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
