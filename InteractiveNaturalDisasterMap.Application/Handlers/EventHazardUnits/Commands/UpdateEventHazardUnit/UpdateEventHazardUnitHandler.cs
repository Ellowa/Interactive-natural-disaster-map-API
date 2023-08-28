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
            if (await _eventHazardUnitRepository.GetByIdAsync(request.UpdateEventHazardUnitDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(EventHazardUnit), request.UpdateEventHazardUnitDto.Id);

            _eventHazardUnitRepository.Update(await request.UpdateEventHazardUnitDto.MapAsync(_unitOfWork, cancellationToken));
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
