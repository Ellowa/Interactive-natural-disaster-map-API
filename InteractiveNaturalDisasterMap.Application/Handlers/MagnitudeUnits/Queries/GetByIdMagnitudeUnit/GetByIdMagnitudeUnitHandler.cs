using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetByIdMagnitudeUnit
{
    public class GetByIdMagnitudeUnitHandler : IRequestHandler<GetByIdMagnitudeUnitRequest, MagnitudeUnitDto>
    {
        private readonly IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;

        public GetByIdMagnitudeUnitHandler(IUnitOfWork unitOfWork)
        {
            _magnitudeUnitRepository = unitOfWork.MagnitudeUnitRepository;
        }

        public async Task<MagnitudeUnitDto> Handle(GetByIdMagnitudeUnitRequest request, CancellationToken cancellationToken)
        {
            var magnitudeUnit = await _magnitudeUnitRepository.GetByIdAsync(request.GetByIdMagnitudeUnitDto.Id, cancellationToken, mu => mu.EventHazardUnits) 
                                ?? throw new NotFoundException(nameof(MagnitudeUnit), request.GetByIdMagnitudeUnitDto.Id);

            return new MagnitudeUnitDto(magnitudeUnit);
        }
    }
}
