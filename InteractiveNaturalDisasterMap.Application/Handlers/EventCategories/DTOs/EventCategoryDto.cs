using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs
{
    public class EventCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<string> MagnitudeUnitsNames { get; set; }

        public EventCategoryDto(EventCategory eventCategory)
        {
            Id = eventCategory.Id;
            CategoryName = eventCategory.CategoryName;
            MagnitudeUnitsNames = new List<string>();
            foreach (var magnitudeUnit  in eventCategory.MagnitudeUnits)
            {
                MagnitudeUnitsNames.Add(magnitudeUnit.MagnitudeUnitName);
            }
        }
    }
}
