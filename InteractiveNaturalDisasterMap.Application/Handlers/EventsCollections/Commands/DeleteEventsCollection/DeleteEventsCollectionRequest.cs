using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteEventsCollection
{
    public class DeleteEventsCollectionRequest : IRequest
    {
        public DeleteEventsCollectionDto DeleteEventsCollectionDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
