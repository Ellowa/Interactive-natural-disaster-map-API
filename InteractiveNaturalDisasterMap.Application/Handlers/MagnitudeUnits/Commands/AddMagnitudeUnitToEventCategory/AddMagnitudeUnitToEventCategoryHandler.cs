using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.AddMagnitudeUnitToEventCategory
{
    public class AddMagnitudeUnitToEventCategoryHandler : IRequestHandler<AddMagnitudeUnitToEventCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public AddMagnitudeUnitToEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _magnitudeUnitRepository = unitOfWork.MagnitudeUnitRepository;
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task Handle(AddMagnitudeUnitToEventCategoryRequest request, CancellationToken cancellationToken)
        {
            var magnitudeUnit = (await _magnitudeUnitRepository.GetAllAsync(cancellationToken,
                    mu => mu.MagnitudeUnitName == request.AddMagnitudeUnitToEventCategoryDto.MagnitudeUnitName, mu => mu.EventCategories))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(MagnitudeUnit),
                    $"With name {request.AddMagnitudeUnitToEventCategoryDto.MagnitudeUnitName}");

            var eventCategory = (await _eventCategoryRepository.GetAllAsync(cancellationToken,
                    ec => ec.CategoryName == request.AddMagnitudeUnitToEventCategoryDto.EventCategoryName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(EventCategory),
                    $"With name {request.AddMagnitudeUnitToEventCategoryDto.EventCategoryName}");

            magnitudeUnit.EventCategories.Add(eventCategory);

            _magnitudeUnitRepository.Update(magnitudeUnit);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
