﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.DeleteEventHazardUnit
{
    public class DeleteEventHazardUnitHandler : IRequestHandler<DeleteEventHazardUnitRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventHazardUnit> _eventHazardUnitRepository;

        public DeleteEventHazardUnitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventHazardUnitRepository = unitOfWork.EventHazardUnitRepository;
        }

        public async Task Handle(DeleteEventHazardUnitRequest request, CancellationToken cancellationToken)
        {
            if(await _eventHazardUnitRepository.GetByIdAsync(request.DeleteEventHazardUnitDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(EventHazardUnit), request.DeleteEventHazardUnitDto.Id);

            var events = await _unitOfWork.NaturalDisasterEventRepository.GetAllByHazardUnitAsync(request.DeleteEventHazardUnitDto.Id, cancellationToken);
            var undefinedEventHazardUnit = (await _eventHazardUnitRepository.GetAllAsync(cancellationToken)).FirstOrDefault(mu => mu.HazardName == "Undefined") 
                                         ?? throw new NotFoundException(nameof(EventHazardUnit), "With name Undefined");

            foreach (NaturalDisasterEvent naturalDisasterEvent in events)
            {
                naturalDisasterEvent.EventHazardUnitId = undefinedEventHazardUnit.Id;
                _unitOfWork.NaturalDisasterEventRepository.Update(naturalDisasterEvent);
            }

            await _eventHazardUnitRepository.DeleteByIdAsync(request.DeleteEventHazardUnitDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}