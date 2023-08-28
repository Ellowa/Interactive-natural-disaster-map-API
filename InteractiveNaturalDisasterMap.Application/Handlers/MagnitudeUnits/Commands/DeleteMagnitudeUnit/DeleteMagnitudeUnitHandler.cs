using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnit
{
    public class DeleteMagnitudeUnitHandler : IRequestHandler<DeleteMagnitudeUnitRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;

        public DeleteMagnitudeUnitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _magnitudeUnitRepository = unitOfWork.MagnitudeUnitRepository;
        }

        public async Task Handle(DeleteMagnitudeUnitRequest request, CancellationToken cancellationToken)
        {
            if(await _magnitudeUnitRepository.GetByIdAsync(request.DeleteMagnitudeUnitDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(MagnitudeUnit), request.DeleteMagnitudeUnitDto.Id);

            var events = await _unitOfWork.NaturalDisasterEventRepository.GetAllByMagnitudeUnitAsync(request.DeleteMagnitudeUnitDto.Id, cancellationToken);
            var undefinedMagnitudeUnit = (await _magnitudeUnitRepository.GetAllAsync(cancellationToken)).FirstOrDefault(mu => mu.MagnitudeUnitName == "Undefined") 
                                         ?? throw new NotFoundException(nameof(MagnitudeUnit), "With name Undefined");

            foreach (NaturalDisasterEvent naturalDisasterEvent in events)
            {
                naturalDisasterEvent.MagnitudeUnitId = undefinedMagnitudeUnit.Id;
                _unitOfWork.NaturalDisasterEventRepository.Update(naturalDisasterEvent);
            }

            await _magnitudeUnitRepository.DeleteByIdAsync(request.DeleteMagnitudeUnitDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
