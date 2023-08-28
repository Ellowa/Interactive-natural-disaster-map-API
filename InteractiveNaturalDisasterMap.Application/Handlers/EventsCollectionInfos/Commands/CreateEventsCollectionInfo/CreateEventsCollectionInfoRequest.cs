using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.CreateEventsCollectionInfo
{
    public class CreateEventsCollectionInfoRequest : IRequest<int>
    {
        public CreateEventsCollectionInfoDto CreateEventsCollectionInfoDto { get; set; } = null!;
    }
}
