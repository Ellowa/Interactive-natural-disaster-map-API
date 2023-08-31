using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs
{
    public class ConfirmUnconfirmedEventDto
    {
        public int EventId { get; set; }
        public bool? Reject { get; set; }
    }
}
