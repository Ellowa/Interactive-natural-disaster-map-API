
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs
{
    public class EventSourceDto
    {
        public int Id { get; set; }
        public string SourceType { get; set; }

        public EventSourceDto(EventSource eventSource)
        {
            Id = eventSource.Id;
            SourceType = eventSource.SourceType;
        }
    }
}
