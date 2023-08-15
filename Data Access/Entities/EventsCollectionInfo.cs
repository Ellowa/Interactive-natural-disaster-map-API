namespace Data_Access.Entities
{
    internal class EventsCollectionInfo : BaseEntity
    {
        public string CollectionName { get; set; }

        public ICollection<EventsCollection> EventsCollection { get; set; }
    }
}
