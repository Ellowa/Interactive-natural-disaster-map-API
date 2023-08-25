
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs
{
    public class EventCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;

        public EventCategoryDto(EventCategory eventCategory)
        {
            Id = eventCategory.Id;
            CategoryName = eventCategory.CategoryName;
        }
    }
}
