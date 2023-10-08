using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.UpdateEventsCollectionInfo
{
    public class UpdateEventsCollectionInfoRequest : IRequest
    {
        public UpdateEventsCollectionInfoDto UpdateEventsCollectionInfoDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
