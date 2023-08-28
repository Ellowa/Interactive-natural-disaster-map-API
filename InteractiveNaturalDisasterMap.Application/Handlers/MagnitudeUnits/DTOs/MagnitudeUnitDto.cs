using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs
{
    public class MagnitudeUnitDto
    {
        public int Id { get; set; }
        public string MagnitudeUnitName { get; set; }
        public List<EventHazardUnitDto> HazardUnitDtos { get; set; }

        public MagnitudeUnitDto(MagnitudeUnit magnitudeUnit)
        {
            Id = magnitudeUnit.Id;
            MagnitudeUnitName = magnitudeUnit.MagnitudeUnitName;
            HazardUnitDtos = new List<EventHazardUnitDto>();
            foreach (var hazardUnit in magnitudeUnit.EventHazardUnits)
            {
                HazardUnitDtos.Add(new EventHazardUnitDto(hazardUnit));
            }
        }
    }
}
