using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs
{
    public class DeleteUnconfirmedEventDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }

        public UnconfirmedEvent Map()
        {
            UnconfirmedEvent unconfirmedEvent = new UnconfirmedEvent()
            {
                EventId = this.EventId,
                UserId = this.UserId,
            };
            return unconfirmedEvent;
        }
    }
}
