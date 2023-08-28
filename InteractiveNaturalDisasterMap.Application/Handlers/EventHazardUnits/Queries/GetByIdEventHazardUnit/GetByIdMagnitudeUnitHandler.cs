using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetByIdEventHazardUnit
{
    public class GetByIdEventHazardUnitHandler : IRequestHandler<GetByIdEventHazardUnitRequest, EventHazardUnitDto>
    {
        private readonly IGenericBaseEntityRepository<EventHazardUnit> _eventHazardUnitRepository;

        public GetByIdEventHazardUnitHandler(IUnitOfWork unitOfWork)
        {
            _eventHazardUnitRepository = unitOfWork.EventHazardUnitRepository;
        }

        public async Task<EventHazardUnitDto> Handle(GetByIdEventHazardUnitRequest request, CancellationToken cancellationToken)
        {
            var magnitudeUnit = await _eventHazardUnitRepository.GetByIdAsync(request.GetByIdEventHazardUnitDto.Id, cancellationToken, ehu => ehu.MagnitudeUnit) 
                                ?? throw new NotFoundException(nameof(EventHazardUnit), request.GetByIdEventHazardUnitDto.Id);

            return new EventHazardUnitDto(magnitudeUnit);
        }
    }
}
