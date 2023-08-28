using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs
{
    public class DeleteEventsCollectionDto
    {
        public int EventId { get; set; }
        public int CollectionId { get; set; }
        public int UserId { get; set; }

        public EventsCollection Map()
        {
            EventsCollection eventsCollection = new EventsCollection()
            {
                EventId = this.EventId,
                CollectionId = this.CollectionId,
            };
            return eventsCollection;
        }
    }
}
