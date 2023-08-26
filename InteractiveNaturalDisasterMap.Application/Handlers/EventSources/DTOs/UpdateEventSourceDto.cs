
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs
{
    public class UpdateEventSourceDto
    {
        public int Id { get; set; }
        public string SourceType { get; set; } = null!;

        public EventSource Map()
        {
            EventSource eventSource = new EventSource()
            {
                Id = this.Id,
                SourceType = this.SourceType,
            };
            return eventSource;
        }
    }
}
