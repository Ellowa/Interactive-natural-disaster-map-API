using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs
{
    public class MagnitudeUnitDto
    {
        public int Id { get; set; }
        public string MagnitudeUnitName { get; set; }

        public MagnitudeUnitDto(MagnitudeUnit magnitudeUnit)
        {
            Id = magnitudeUnit.Id;
            MagnitudeUnitName = magnitudeUnit.MagnitudeUnitName;
        }
    }
}
