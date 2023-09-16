using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Validators
{
    public sealed class UpdateEventsCollectionInfoValidator : AbstractValidator<UpdateEventsCollectionInfoDto>
    {
        public UpdateEventsCollectionInfoValidator()
        {
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.CollectionName).NotEmpty();
        }
    }
}
