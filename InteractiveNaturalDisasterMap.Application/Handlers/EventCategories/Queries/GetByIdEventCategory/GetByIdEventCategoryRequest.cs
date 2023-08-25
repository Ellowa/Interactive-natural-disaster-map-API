
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetByIdEventCategory
{
    public class GetByIdEventCategoryRequest : IRequest<EventCategoryDto>
    {
        public GetByIdEventCategoryDto GetByIdEventCategoryDto { get; set; } = null!;
    }
}
