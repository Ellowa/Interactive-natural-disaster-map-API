using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.UpdateEventHazardUnit;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Validators
{
    public sealed class UpdateEventHazardUnitValidator : AbstractValidator<UpdateEventHazardUnitRequest>
    {
        public UpdateEventHazardUnitValidator()
        {
            RuleFor(c => c.UpdateEventHazardUnitDto.Id).NotNull();
            RuleFor(c => c.UpdateEventHazardUnitDto.HazardName).NotEmpty();
            RuleFor(c => c.UpdateEventHazardUnitDto.MagnitudeUnitName).NotEmpty();
        }
    }
}
