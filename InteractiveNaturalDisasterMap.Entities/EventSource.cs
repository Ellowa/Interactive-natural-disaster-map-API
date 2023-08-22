namespace InteractiveNaturalDisasterMap.Entities
{
    public class EventSource : BaseEntity
    {
        public string SourceType { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;
    }
}
