using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnitFromEventCategory
{
    public class DeleteMagnitudeUnitFromEventCategoryHandler : IRequestHandler<DeleteMagnitudeUnitFromEventCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public DeleteMagnitudeUnitFromEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _magnitudeUnitRepository = unitOfWork.MagnitudeUnitRepository;
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task Handle(DeleteMagnitudeUnitFromEventCategoryRequest request, CancellationToken cancellationToken)
        {
            var magnitudeUnit = (await _magnitudeUnitRepository.GetAllAsync(cancellationToken,
                    mu => mu.MagnitudeUnitName == request.DeleteMagnitudeUnitFromEventCategoryDto.MagnitudeUnitName, mu => mu.EventCategories))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(MagnitudeUnit),
                    $"With name {request.DeleteMagnitudeUnitFromEventCategoryDto.MagnitudeUnitName}");

            var eventCategory = (await _eventCategoryRepository.GetAllAsync(cancellationToken,
                    ec => ec.CategoryName == request.DeleteMagnitudeUnitFromEventCategoryDto.EventCategoryName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(EventCategory),
                    $"With name {request.DeleteMagnitudeUnitFromEventCategoryDto.EventCategoryName}");

            if (magnitudeUnit.EventCategories.Contains(eventCategory))
            {
                magnitudeUnit.EventCategories.Remove(eventCategory);
            }
            else
            {
                throw new NotFoundException(nameof(EventCategory),
                    $"With name {request.DeleteMagnitudeUnitFromEventCategoryDto.EventCategoryName} in this {request.DeleteMagnitudeUnitFromEventCategoryDto.MagnitudeUnitName} magnitudeUnit");
            }

            _magnitudeUnitRepository.Update(magnitudeUnit);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
