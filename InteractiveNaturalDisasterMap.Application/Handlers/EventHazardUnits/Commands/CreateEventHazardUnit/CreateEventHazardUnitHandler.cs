using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.CreateEventHazardUnit
{
    public class CreateEventHazardUnitHandler : IRequestHandler<CreateEventHazardUnitRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventHazardUnit> _eventHazardUnitRepository;

        public CreateEventHazardUnitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventHazardUnitRepository = unitOfWork.EventHazardUnitRepository;
        }

        public async Task<int> Handle(CreateEventHazardUnitRequest request, CancellationToken cancellationToken)
        {
            var entity = await request.CreateEventHazardUnitDto.MapAsync(_unitOfWork, cancellationToken);
            await _eventHazardUnitRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
