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
            var eventHazardUnit = await _eventHazardUnitRepository.GetByIdAsync(request.UpdateEventHazardUnitDto.Id, cancellationToken,
                    ehu => ehu.MagnitudeUnit) 
                                  ?? throw new NotFoundException(nameof(EventHazardUnit), request.UpdateEventHazardUnitDto.Id);

            var magnitudeUnit = (await _unitOfWork.MagnitudeUnitRepository.GetAllAsync(cancellationToken,
                    mu => mu.MagnitudeUnitName == request.UpdateEventHazardUnitDto.MagnitudeUnitName))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(MagnitudeUnit), $"With name {request.UpdateEventHazardUnitDto.MagnitudeUnitName}");

            eventHazardUnit.HazardName = request.UpdateEventHazardUnitDto.HazardName;
            eventHazardUnit.MagnitudeUnitId = magnitudeUnit.Id;
            eventHazardUnit.ThresholdValue = request.UpdateEventHazardUnitDto.ThresholdValue;

            _eventHazardUnitRepository.Update(eventHazardUnit);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
