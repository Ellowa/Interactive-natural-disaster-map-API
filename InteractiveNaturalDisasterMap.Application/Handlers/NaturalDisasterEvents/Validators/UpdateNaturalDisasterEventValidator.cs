using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.UpdateNaturalDisasterEvent;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Validators
{
    public sealed class UpdateNaturalDisasterEventValidator : AbstractValidator<UpdateNaturalDisasterEventRequest>
    {
        public UpdateNaturalDisasterEventValidator()
        {
            RuleFor(u => u.UpdateNaturalDisasterEventDto.Title).NotEmpty();
            RuleFor(u => u.UpdateNaturalDisasterEventDto.StartDate).LessThanOrEqualTo(DateTime.Now.ToUniversalTime());
            RuleFor(u => u.UpdateNaturalDisasterEventDto.EndDate)
                .GreaterThan(u => u.UpdateNaturalDisasterEventDto.StartDate)
                .LessThanOrEqualTo(DateTime.Now.ToUniversalTime()).When(u => u.UpdateNaturalDisasterEventDto.EndDate != null);
            RuleFor(u => u.UpdateNaturalDisasterEventDto.MagnitudeValue)
                .GreaterThan(0)
                .When(u => u.UpdateNaturalDisasterEventDto.MagnitudeValue != null);
            RuleFor(u => u.UpdateNaturalDisasterEventDto.EventCategoryName).NotEmpty();
            RuleFor(u => u.UpdateNaturalDisasterEventDto.MagnitudeUnitName).NotEmpty();
            RuleFor(u => u.UpdateNaturalDisasterEventDto.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(u => u.UpdateNaturalDisasterEventDto.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
