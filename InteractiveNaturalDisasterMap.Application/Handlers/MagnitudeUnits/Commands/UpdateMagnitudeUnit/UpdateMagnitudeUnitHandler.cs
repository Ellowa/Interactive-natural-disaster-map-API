using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.UpdateMagnitudeUnit
{
    public class UpdateMagnitudeUnitHandler : IRequestHandler<UpdateMagnitudeUnitRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;

        public UpdateMagnitudeUnitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _magnitudeUnitRepository = unitOfWork.MagnitudeUnitRepository;
        }

        public async Task Handle(UpdateMagnitudeUnitRequest request, CancellationToken cancellationToken)
        {
            if (await _magnitudeUnitRepository.GetByIdAsync(request.UpdateMagnitudeUnitDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(MagnitudeUnit), request.UpdateMagnitudeUnitDto.Id);

            _magnitudeUnitRepository.Update(request.UpdateMagnitudeUnitDto.Map());
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
