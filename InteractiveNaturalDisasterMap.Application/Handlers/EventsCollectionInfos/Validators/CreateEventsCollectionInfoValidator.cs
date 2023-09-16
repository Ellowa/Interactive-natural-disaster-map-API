using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Validators
{
    public sealed class CreateEventsCollectionInfoValidator : AbstractValidator<CreateEventsCollectionInfoDto>
    {
        public CreateEventsCollectionInfoValidator()
        {
            RuleFor(c => c.CollectionName).NotEmpty();
        }
    }
}
