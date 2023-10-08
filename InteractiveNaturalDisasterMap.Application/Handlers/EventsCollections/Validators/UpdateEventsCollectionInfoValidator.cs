using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.UpdateEventsCollectionInfo;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Validators
{
    public sealed class UpdateEventsCollectionInfoValidator : AbstractValidator<UpdateEventsCollectionInfoRequest>
    {
        public UpdateEventsCollectionInfoValidator()
        {
            RuleFor(c => c.UpdateEventsCollectionInfoDto.Id).NotNull();
            RuleFor(c => c.UpdateEventsCollectionInfoDto.CollectionName).NotEmpty();
        }
    }
}
