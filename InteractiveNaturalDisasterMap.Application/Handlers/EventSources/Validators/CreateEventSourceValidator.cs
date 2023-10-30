using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.CreateEventSource;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Validators
{
    public sealed class CreateEventHazardUnitsValidator : AbstractValidator<CreateEventSourceRequest>
    {
        public CreateEventHazardUnitsValidator()
        {
            RuleFor(c => c.CreateEventSourceDto.SourceType).NotEmpty();
        }
    }
}
