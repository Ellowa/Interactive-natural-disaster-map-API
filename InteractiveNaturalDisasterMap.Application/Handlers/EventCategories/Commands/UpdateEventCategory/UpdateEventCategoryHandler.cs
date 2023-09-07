using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.UpdateEventCategory
{
    public class UpdateEventCategoryHandler : IRequestHandler<UpdateEventCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public UpdateEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task Handle(UpdateEventCategoryRequest request, CancellationToken cancellationToken)
        {
            var eventCategory = await _eventCategoryRepository.GetByIdAsync(request.UpdateEventCategoryDto.Id, cancellationToken) 
                                ?? throw new NotFoundException(nameof(EventCategory), request.UpdateEventCategoryDto.Id);

            eventCategory.CategoryName = request.UpdateEventCategoryDto.CategoryName;

            _eventCategoryRepository.Update(eventCategory);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
