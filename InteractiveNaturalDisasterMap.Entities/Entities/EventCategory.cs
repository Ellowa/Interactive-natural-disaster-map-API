namespace InteractiveNaturalDisasterMap.Domain.Entities
{
    public class EventCategory : BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;

        public ICollection<MagnitudeUnit> MagnitudeUnits { get; set; } = null!;
    }
}
