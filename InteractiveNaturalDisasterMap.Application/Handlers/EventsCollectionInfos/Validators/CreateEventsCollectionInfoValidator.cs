using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.CreateEventsCollectionInfo;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Validators
{
    public sealed class CreateEventsCollectionInfoValidator : AbstractValidator<CreateEventsCollectionInfoRequest>
    {
        public CreateEventsCollectionInfoValidator()
        {
            RuleFor(c => c.CreateEventsCollectionInfoDto.CollectionName).NotEmpty();
        }
    }
}
