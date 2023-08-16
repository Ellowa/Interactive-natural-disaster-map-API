namespace Data_Access.Entities
{
    public class EventsCollection
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public int CollectionId { get; set; }

        public NaturalDisasterEvent Event { get; set; } = null!;

        public User User { get; set; } = null!;

        public EventsCollectionInfo EventsCollectionInfo { get; set; } = null!;

    }
}
