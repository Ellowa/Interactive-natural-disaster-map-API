using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.CreateEventsCollection
{
    public class CreateEventsCollectionRequest : IRequest
    {
        public CreateEventsCollectionDto CreateEventsCollectionDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
