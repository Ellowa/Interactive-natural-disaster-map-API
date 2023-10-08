using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.CreateEventsCollectionInfo
{
    public class CreateEventsCollectionInfoRequest : IRequest<int>
    {
        public CreateEventsCollectionInfoDto CreateEventsCollectionInfoDto { get; set; } = null!;

        public int UserId { get; set; }
    }
}
