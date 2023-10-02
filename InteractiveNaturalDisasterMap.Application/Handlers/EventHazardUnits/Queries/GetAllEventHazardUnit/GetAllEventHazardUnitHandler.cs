using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

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
            IEnumerable<EventHazardUnit> hazardUnits = await _eventHazardUnitRepository.GetAllAsync(cancellationToken, null, ehu => ehu.MagnitudeUnit);

            hazardUnits = request.GetAllEventHazardUnitDto.SortOrder?.ToLower() == "asc"
                ? hazardUnits.OrderBy(GetSortProperty(request).Compile())
                : hazardUnits.OrderByDescending(GetSortProperty(request).Compile());

            IList<EventHazardUnitDto> hazardUnitDtos = new List<EventHazardUnitDto>(); 
            foreach (var hazardUnit in hazardUnits)
            {
                hazardUnitDtos.Add(new EventHazardUnitDto(hazardUnit));
            }

            return hazardUnitDtos;
        }

        private static Expression<Func<EventHazardUnit, object>> GetSortProperty(GetAllEventHazardUnitRequest request)
        {
            return request.GetAllEventHazardUnitDto.SortColumn?.ToLower() switch
            {
                "name" => nde => nde.HazardName,
                "magnitude" => nde => nde.MagnitudeUnitId,
                _ => nde => nde.Id
            };
        }
    }
}
