using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.UpdateEventCategory
{
    public class UpdateEventCategoryRequest : IRequest
    {
        public UpdateEventCategoryDto UpdateEventCategoryDto { get; set; } = null!;
    }
}
