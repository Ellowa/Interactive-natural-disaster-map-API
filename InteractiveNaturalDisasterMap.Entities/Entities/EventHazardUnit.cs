
namespace InteractiveNaturalDisasterMap.Domain.Entities
{
    public class EventHazardUnit : BaseEntity
    {
        public string HazardName { get; set; } = null!;

        public int MagnitudeUnitId { get; set; }

        public double? ThresholdValue { get; set; }

        public MagnitudeUnit MagnitudeUnit { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> NaturalDisasterEvents { get; set; } = null!;
    }
}
