namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs
{
    public class UpdateMagnitudeUnitDto
    {
        public int Id { get; set; }
        public string MagnitudeUnitName { get; set; } = null!;
        public string MagnitudeUnitDescription { get; set; } = null!;
    }
}
