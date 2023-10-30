using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetAllEventCategory
{
    public class GetAllEventCategoryHandler : IRequestHandler<GetAllEventCategoryRequest, IList<EventCategoryDto>>
    {
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public GetAllEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task<IList<EventCategoryDto>> Handle(GetAllEventCategoryRequest request, CancellationToken cancellationToken)
        {
            var eventCategories = (await _eventCategoryRepository.GetAllAsync(
                    cancellationToken,
                    null,
                    ec => ec.MagnitudeUnits))
                .OrderBy(ec => ec.Id);
            IList<EventCategoryDto> eventCategoryDtos = new List<EventCategoryDto>();
            foreach (var eventCategory in eventCategories)
            {
                eventCategoryDtos.Add(new EventCategoryDto(eventCategory));
            }

            return eventCategoryDtos;
        }
    }
}
