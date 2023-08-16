namespace Data_Access.Entities
{
    public class EventCategory : BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;
    }
}
