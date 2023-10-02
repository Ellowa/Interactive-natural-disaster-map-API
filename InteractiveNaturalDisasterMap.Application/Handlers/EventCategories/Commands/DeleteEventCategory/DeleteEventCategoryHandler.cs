using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Utilities;
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
            if(await _eventCategoryRepository.GetByIdAsync(request.DeleteEventCategoryDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(EventCategory), request.DeleteEventCategoryDto.Id);

            var events = await _unitOfWork.NaturalDisasterEventRepository.GetAllAsync(cancellationToken, ndu => ndu.EventCategoryId == request.DeleteEventCategoryDto.Id);
            var otherCategory = (await _eventCategoryRepository.GetAllAsync(cancellationToken, mu => mu.CategoryName == EntityNamesByDefault.DefaultEventCategory)).FirstOrDefault()
                                           ?? throw new NotFoundException(nameof(EventCategory), $"With name {EntityNamesByDefault.DefaultEventCategory}");

            foreach (NaturalDisasterEvent naturalDisasterEvent in events)
            {
                naturalDisasterEvent.EventCategoryId = otherCategory.Id;
                _unitOfWork.NaturalDisasterEventRepository.Update(naturalDisasterEvent);
            }

            await _eventCategoryRepository.DeleteByIdAsync(request.DeleteEventCategoryDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
