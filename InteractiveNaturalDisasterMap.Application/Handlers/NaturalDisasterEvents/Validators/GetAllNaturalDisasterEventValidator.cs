using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetAllNaturalDisasterEvent;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Validators
{
    internal class GetAllNaturalDisasterEventValidator : AbstractValidator<GetAllNaturalDisasterEventRequest>
    {
        public GetAllNaturalDisasterEventValidator()
        {
            RuleFor(g => g.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint)
                .LessThan(DateTime.UtcNow.ToUniversalTime())
                .GreaterThanOrEqualTo((DateTime.Now - TimeSpan.FromDays(1827)).ToUniversalTime())
                .When(g => g.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint != null);
        }
    }
}
