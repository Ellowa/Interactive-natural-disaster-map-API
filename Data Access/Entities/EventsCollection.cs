namespace Data_Access.Entities
{
    public class EventsCollection
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public int CollectionId { get; set; }

        public Event Event { get; set; }

        public User User { get; set; }

        public EventsCollectionInfo EventsCollectionInfo { get; set; }

    }
}
