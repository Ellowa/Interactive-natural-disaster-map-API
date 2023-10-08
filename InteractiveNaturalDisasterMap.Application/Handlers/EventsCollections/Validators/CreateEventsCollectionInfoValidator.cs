using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.CreateEventsCollectionInfo;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Validators
{
    public sealed class CreateEventsCollectionInfoValidator : AbstractValidator<CreateEventsCollectionInfoRequest>
    {
        public CreateEventsCollectionInfoValidator()
        {
            RuleFor(c => c.CreateEventsCollectionInfoDto.CollectionName).NotEmpty();
        }
    }
}
