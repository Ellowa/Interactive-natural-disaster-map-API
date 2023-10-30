namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs
{
    public class UpdateNaturalDisasterEventDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? MagnitudeValue { get; set; }

        public string EventCategoryName { get; set; } = string.Empty;

        public string MagnitudeUnitName { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
