
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs
{
    public class UpdateEventCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
