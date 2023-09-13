namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs
{
    public class ConfirmOrRejectUnconfirmedEventDto
    {
        public int EventId { get; set; }
        public bool? Reject { get; set; }
    }
}
