using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs
{
    public class CreateEventCategoryDto
    {
        public string CategoryName { get; set; } = null!;

        public EventCategory Map()
        {
            EventCategory eventCategory = new EventCategory()
            {
                CategoryName = this.CategoryName,
            };
            return eventCategory;
        }
    }
}
