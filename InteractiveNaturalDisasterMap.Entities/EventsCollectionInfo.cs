namespace InteractiveNaturalDisasterMap.Entities
{
    public class EventsCollectionInfo : BaseEntity
    {
        public string CollectionName { get; set; } = null!;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public ICollection<EventsCollection> EventsCollection { get; set; } = null!;
    }
}
