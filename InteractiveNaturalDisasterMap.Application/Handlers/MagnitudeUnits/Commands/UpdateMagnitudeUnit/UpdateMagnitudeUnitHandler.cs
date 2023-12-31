﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
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
            var magnitudeUnit = await _magnitudeUnitRepository.GetByIdAsync(request.UpdateMagnitudeUnitDto.Id, cancellationToken) 
                                ?? throw new NotFoundException(nameof(MagnitudeUnit), request.UpdateMagnitudeUnitDto.Id);

            magnitudeUnit.MagnitudeUnitName = request.UpdateMagnitudeUnitDto.MagnitudeUnitName;
            magnitudeUnit.MagnitudeUnitDescription = request.UpdateMagnitudeUnitDto.MagnitudeUnitDescription;
            _magnitudeUnitRepository.Update(magnitudeUnit);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
