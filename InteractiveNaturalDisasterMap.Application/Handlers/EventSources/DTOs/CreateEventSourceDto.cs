using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs
{
    public class CreateEventSourceDto
    {
        public string SourceType { get; set; } = null!;

        public EventSource Map()
        {
            EventSource eventSource = new EventSource()
            {
                SourceType = this.SourceType,
            };
            return eventSource;
        }
    }
}
