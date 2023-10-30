using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.UpdateEventSource;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Validators
{
    public sealed class UpdateEventSourceValidator : AbstractValidator<UpdateEventSourceRequest>
    {
        public UpdateEventSourceValidator()
        {
            RuleFor(c => c.UpdateEventSourceDto.Id).NotNull();
            RuleFor(c => c.UpdateEventSourceDto.SourceType).NotEmpty();
        }
    }
}
