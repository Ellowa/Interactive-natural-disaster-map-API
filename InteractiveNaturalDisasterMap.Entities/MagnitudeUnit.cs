namespace InteractiveNaturalDisasterMap.Entities
{
    public class MagnitudeUnit : BaseEntity
    {
        public string MagnitudeUnitName { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;
    }
}
