﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetAllMagnitudeUnit
{
    public class GetAllMagnitudeUnitHandler : IRequestHandler<GetAllMagnitudeUnitRequest, IList<MagnitudeUnitDto>>
    {
        private readonly IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;

        public GetAllMagnitudeUnitHandler(IUnitOfWork unitOfWork)
        {
            _magnitudeUnitRepository = unitOfWork.MagnitudeUnitRepository;
        }

        public async Task<IList<MagnitudeUnitDto>> Handle(GetAllMagnitudeUnitRequest request, CancellationToken cancellationToken)
        {
            var eventCategories = await _magnitudeUnitRepository.GetAllAsync(cancellationToken);
            IList<MagnitudeUnitDto> magnitudeUnitDtos = new List<MagnitudeUnitDto>(); 
            foreach (var magnitudeUnit in eventCategories)
            {
                magnitudeUnitDtos.Add(new MagnitudeUnitDto(magnitudeUnit));
            }

            return magnitudeUnitDtos;
        }
    }
}