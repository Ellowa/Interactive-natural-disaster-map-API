using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteEventsCollectionInfo
{
    public class DeleteEventsCollectionInfoRequest : IRequest
    {
        public DeleteEventsCollectionInfoDto DeleteEventsCollectionInfoDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
