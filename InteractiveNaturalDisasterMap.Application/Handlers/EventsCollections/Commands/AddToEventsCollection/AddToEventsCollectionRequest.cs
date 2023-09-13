using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.AddToEventsCollection
{
    public class AddToEventsCollectionRequest : IRequest
    {
        public AddToEventsCollectionDto AddToEventsCollectionDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
