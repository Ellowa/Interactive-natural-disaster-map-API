namespace Data_Access.Entities
{
    internal class Event : BaseEntity
    {
        public string Title { get; set; }

        public string? Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? MagnitudeValue { get; set; }

        public int EventCategoryId { get; set; }

        public int SourceId { get; set; }

        public int? MagnitudeUnitId { get; set; }

        public int CoordinateId { get; set; }

        public int? UserId { get; set; }

        public EventCategory Category { get; set; }

        public Source Source { get; set; }

        public MagnitudeUnit MagnitudeUnit { get; set; } = null;

        public Coordinate Coordinate { get; set; }

        public User User { get; set; } = null;

        public ICollection<EventsCollection> EventsCollection { get; set; }
    }
}
