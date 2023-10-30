using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.UpdateMagnitudeUnit;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Validators
{
    public sealed class UpdateMagnitudeUnitValidator : AbstractValidator<UpdateMagnitudeUnitRequest>
    {
        public UpdateMagnitudeUnitValidator()
        {
            RuleFor(c => c.UpdateMagnitudeUnitDto.Id).NotNull();
            RuleFor(c => c.UpdateMagnitudeUnitDto.MagnitudeUnitName).NotEmpty();
        }
    }
}
