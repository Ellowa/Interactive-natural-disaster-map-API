namespace Data_Access.Entities
{
    internal class EventCategory : BaseEntity
    {
        public string CategoryName { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
