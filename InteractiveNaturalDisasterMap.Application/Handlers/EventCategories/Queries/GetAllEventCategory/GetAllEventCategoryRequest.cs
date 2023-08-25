﻿using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetAllEventCategory
{
    public class GetAllEventCategoryRequest : IRequest<IList<EventCategoryDto>>
    {
    }
}
