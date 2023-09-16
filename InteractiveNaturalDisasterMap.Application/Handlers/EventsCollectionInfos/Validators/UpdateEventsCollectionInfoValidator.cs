using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.UpdateEventsCollectionInfo;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Validators
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
