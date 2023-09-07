namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs
{
    public class UpdateEventSourceDto
    {
        public int Id { get; set; }
        public string SourceType { get; set; } = null!;
    }
}
