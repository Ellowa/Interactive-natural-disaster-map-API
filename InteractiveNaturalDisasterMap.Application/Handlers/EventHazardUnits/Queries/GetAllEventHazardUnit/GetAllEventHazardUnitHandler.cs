using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetAllEventHazardUnit
{
    public class GetAllEventHazardUnitHandler : IRequestHandler<GetAllEventHazardUnitRequest, IList<EventHazardUnitDto>>
    {
        private readonly IGenericBaseEntityRepository<EventHazardUnit> _eventHazardUnitRepository;

        public GetAllEventHazardUnitHandler(IUnitOfWork unitOfWork)
        {
            _eventHazardUnitRepository = unitOfWork.EventHazardUnitRepository;
        }

        public async Task<IList<EventHazardUnitDto>> Handle(GetAllEventHazardUnitRequest request, CancellationToken cancellationToken)
        {
            var hazardUnits = await _eventHazardUnitRepository.GetAllAsync(cancellationToken, null, ehu => ehu.MagnitudeUnit);
            IList<EventHazardUnitDto> hazardUnitDtos = new List<EventHazardUnitDto>(); 
            foreach (var hazardUnit in hazardUnits)
            {
                hazardUnitDtos.Add(new EventHazardUnitDto(hazardUnit));
            }

            return hazardUnitDtos;
        }
    }
}
