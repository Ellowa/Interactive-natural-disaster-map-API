using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteFromEventsCollection
{
    public class DeleteFromEventsCollectionRequest : IRequest
    {
        public DeleteFromEventsCollectionDto DeleteFromEventsCollectionDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
