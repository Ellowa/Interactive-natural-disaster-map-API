using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory
{
    public class CreateEventCategoryRequest : IRequest<int>
    {
        public CreateEventCategoryDto CreateEventCategoryDto { get; set; } = null!;
    }
}
