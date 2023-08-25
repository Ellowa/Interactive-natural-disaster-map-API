namespace InteractiveNaturalDisasterMap.Domain.Entities
{
    public class UnconfirmedEvent
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public NaturalDisasterEvent Event { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
