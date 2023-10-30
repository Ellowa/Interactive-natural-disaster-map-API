using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.CreateEventHazardUnit;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Validators
{
    public sealed class CreateEventHazardUnitsValidator : AbstractValidator<CreateEventHazardUnitRequest>
    {
        public CreateEventHazardUnitsValidator()
        {
            RuleFor(c => c.CreateEventHazardUnitDto.HazardName).NotEmpty();
            RuleFor(c => c.CreateEventHazardUnitDto.MagnitudeUnitName).NotEmpty();
        }
    }
}
