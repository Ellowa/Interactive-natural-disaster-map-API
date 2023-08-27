using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs
{
    public class UpdateMagnitudeUnitDto
    {
        public int Id { get; set; }
        public string MagnitudeUnitName { get; set; } = null!;

        public MagnitudeUnit Map()
        {
            MagnitudeUnit magnitudeUnit = new MagnitudeUnit()
            {
                Id = this.Id,
                MagnitudeUnitName = this.MagnitudeUnitName,
            };
            return magnitudeUnit;
        }
    }
}
