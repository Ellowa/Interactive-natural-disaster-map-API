using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs
{
    public class CreateMagnitudeUnitDto
    {
        public string MagnitudeUnitName { get; set; } = null!;

        public MagnitudeUnit Map()
        {
            MagnitudeUnit magnitudeUnit = new MagnitudeUnit()
            {
                MagnitudeUnitName = this.MagnitudeUnitName,
            };
            return magnitudeUnit;
        }
    }
}
