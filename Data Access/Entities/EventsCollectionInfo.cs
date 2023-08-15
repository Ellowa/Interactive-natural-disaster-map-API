namespace Data_Access.Entities
{
    internal class EventsCollectionInfo : BaseEntity
    {
        public string CollectionName { get; set; }

        public EventsCollection EventsCollection { get; set; }
    }
}
