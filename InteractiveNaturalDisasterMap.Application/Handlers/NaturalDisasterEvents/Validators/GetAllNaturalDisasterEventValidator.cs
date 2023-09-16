using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Validators
{
    internal class GetAllNaturalDisasterEventValidator : AbstractValidator<GetAllNaturalDisasterEventDto>
    {
        public GetAllNaturalDisasterEventValidator()
        {
            RuleFor(g => g.ExtendedPeriodEndPoint)
                .LessThan((DateTime.UtcNow - TimeSpan.FromDays(364)).ToUniversalTime())
                .GreaterThanOrEqualTo((DateTime.Now - TimeSpan.FromDays(1827)).ToUniversalTime())
                .When(g => g.ExtendedPeriodEndPoint != null);
        }
    }
}
