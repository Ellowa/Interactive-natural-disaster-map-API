using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.DeleteEventCategory
{
    public class DeleteEventCategoryHandler : IRequestHandler<DeleteEventCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public DeleteEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task Handle(DeleteEventCategoryRequest request, CancellationToken cancellationToken)
        {
            if(await _eventCategoryRepository.GetByIdAsync(request.DeleteEventCategoryDto.Id) == null)
                throw new NotFoundException(nameof(EventCategory), request.DeleteEventCategoryDto.Id);

            await _eventCategoryRepository.DeleteByIdAsync(request.DeleteEventCategoryDto.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
