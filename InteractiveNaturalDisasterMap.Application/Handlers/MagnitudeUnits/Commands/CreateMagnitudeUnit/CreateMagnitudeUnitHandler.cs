using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.CreateMagnitudeUnit
{
    public class CreateMagnitudeUnitHandler : IRequestHandler<CreateMagnitudeUnitRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;

        public CreateMagnitudeUnitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _magnitudeUnitRepository = unitOfWork.MagnitudeUnitRepository;
        }

        public async Task<int> Handle(CreateMagnitudeUnitRequest request, CancellationToken cancellationToken)
        {
            var entity = request.CreateMagnitudeUnitDto.Map();
            await _magnitudeUnitRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
