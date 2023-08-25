using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.DeleteEventCategory
{
    public class DeleteEventCategoryRequest : IRequest
    {
        public DeleteEventCategoryDto DeleteEventCategoryDto { get; set; } = null!;
    }
}
