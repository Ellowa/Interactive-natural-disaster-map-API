using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.UpdateEventHazardUnit
{
    public class UpdateEventHazardUnitHandler : IRequestHandler<UpdateEventHazardUnitRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventHazardUnit> _eventHazardUnitRepository;

        public UpdateEventHazardUnitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventHazardUnitRepository = unitOfWork.EventHazardUnitRepository;
        }

        public async Task Handle(UpdateEventHazardUnitRequest request, CancellationToken cancellationToken)
        {
            var oldEntity = (await _eventHazardUnitRepository.GetByIdAsync(request.UpdateEventHazardUnitDto.Id, cancellationToken, ehu=> ehu.MagnitudeUnit))
                            ?? throw new NotFoundException(nameof(EventHazardUnit), request.UpdateEventHazardUnitDto.Id);

            var newEntity = request.UpdateEventHazardUnitDto;
            newEntity.HazardName ??= oldEntity.HazardName;
            newEntity.MagnitudeUnitName ??= oldEntity.MagnitudeUnit.MagnitudeUnitName;

            _eventHazardUnitRepository.Update(await newEntity.MapAsync(_unitOfWork, cancellationToken));
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
