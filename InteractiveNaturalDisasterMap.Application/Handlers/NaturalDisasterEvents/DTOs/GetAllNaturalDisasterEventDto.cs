namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs
{
    public class GetAllNaturalDisasterEventDto
    {
        public DateTime? ExtendedPeriodEndPoint { get; set; }

        public string? SortColumn { get; set; }

        public string? SortOrder { get; set; }
    }
}
