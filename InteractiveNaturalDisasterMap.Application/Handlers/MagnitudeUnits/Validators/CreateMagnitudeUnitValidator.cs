using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.CreateMagnitudeUnit;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Validators
{
    public sealed class CreateEventHazardUnitsValidator : AbstractValidator<CreateMagnitudeUnitRequest>
    {
        public CreateEventHazardUnitsValidator()
        {
            RuleFor(c => c.CreateMagnitudeUnitDto.MagnitudeUnitName).NotEmpty();
        }
    }
}
