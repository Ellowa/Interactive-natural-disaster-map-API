namespace Data_Access.Entities
{
    public class EventsCollectionInfo : BaseEntity
    {
        public string CollectionName { get; set; } = null!;

        public ICollection<EventsCollection> EventsCollection { get; set; } = null!;
    }
}
