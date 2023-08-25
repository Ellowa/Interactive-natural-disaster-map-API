﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetByIdEventCategory
{
    public class GetByIdEventCategoryHandler : IRequestHandler<GetByIdEventCategoryRequest, EventCategoryDto>
    {
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public GetByIdEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task<EventCategoryDto> Handle(GetByIdEventCategoryRequest request, CancellationToken cancellationToken)
        {
            var eventCategory = await _eventCategoryRepository.GetByIdAsync(request.GetByIdEventCategoryDto.Id) 
                                ?? throw new NotFoundException("This event category was not found");

            return new EventCategoryDto(eventCategory);
        }
    }
}
