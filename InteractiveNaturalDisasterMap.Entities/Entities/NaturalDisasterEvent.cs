namespace InteractiveNaturalDisasterMap.Domain.Entities
{
    public class NaturalDisasterEvent : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string? Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? MagnitudeValue { get; set; }

        public bool Confirmed { get; set; }

        public int EventCategoryId { get; set; }

        public int SourceId { get; set; }

        public int MagnitudeUnitId { get; set; }

        public int CoordinateId { get; set; }

        public EventCategory Category { get; set; } = null!;

        public EventSource Source { get; set; } = null!;

        public MagnitudeUnit MagnitudeUnit { get; set; } = null!;

        public EventCoordinate Coordinate { get; set; } = null!;

        public UnconfirmedEvent UnconfirmedEvent { get; set; } = null!;

        public ICollection<EventsCollection> EventsCollection { get; set; } = null!;
    }
}
