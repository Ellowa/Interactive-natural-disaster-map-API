namespace Data_Access.Entities
{
    public class EventCategory : BaseEntity
    {
        public string CategoryName { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
