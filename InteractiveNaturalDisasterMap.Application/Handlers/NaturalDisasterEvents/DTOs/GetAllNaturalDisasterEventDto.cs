namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs
{
    public class GetAllNaturalDisasterEventDto
    {
        public int? UserId { get; set; }

        public DateTime? ExtendedPeriodEndPoint { get; set; }
    }
}
