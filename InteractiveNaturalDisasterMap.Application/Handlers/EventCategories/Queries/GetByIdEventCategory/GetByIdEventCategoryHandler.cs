using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
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
            var eventCategory =
                await _eventCategoryRepository.GetByIdAsync(request.GetByIdEventCategoryDto.Id, cancellationToken, ec => ec.MagnitudeUnits)
                ?? throw new NotFoundException(nameof(EventCategory), request.GetByIdEventCategoryDto.Id);

            return new EventCategoryDto(eventCategory);
        }
    }
}
