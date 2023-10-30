using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs
{
    public class MagnitudeUnitDto
    {
        public int Id { get; set; }
        public string MagnitudeUnitName { get; set; }
        public string MagnitudeUnitDescription { get; set; }
        public List<EventHazardUnitDto> HazardUnitDtos { get; set; }
        public List<EventCategoryDto> EventCategoryDtos { get; set; }

        public MagnitudeUnitDto(MagnitudeUnit magnitudeUnit)
        {
            Id = magnitudeUnit.Id;
            MagnitudeUnitName = magnitudeUnit.MagnitudeUnitName;
            MagnitudeUnitDescription = magnitudeUnit.MagnitudeUnitDescription;
            HazardUnitDtos = new List<EventHazardUnitDto>();
            foreach (var hazardUnit in magnitudeUnit.EventHazardUnits)
            {
                HazardUnitDtos.Add(new EventHazardUnitDto(hazardUnit));
            }
            EventCategoryDtos = new List<EventCategoryDto>();
            foreach (var eventCategory in magnitudeUnit.EventCategories)
            {
                EventCategoryDtos.Add(new EventCategoryDto(eventCategory));
            }
        }
    }
}
