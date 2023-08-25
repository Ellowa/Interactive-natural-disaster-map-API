using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory
{
    public class CreateEventCategoryHandler : IRequestHandler<CreateEventCategoryRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public CreateEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task<int> Handle(CreateEventCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = request.CreateEventCategoryDto.Map();
            await _eventCategoryRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
