﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory
{
    public class CreateEventCategoryHandler : IRequestHandler<CreateEventCategoryRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;

        public CreateEventCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventCategoryRepository = unitOfWork.EventCategoryRepository;
        }

        public async Task<int> Handle(CreateEventCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = request.CreateEventCategoryDto.Map();

            var undefinedMagnitudeUnit =
                (await _unitOfWork.MagnitudeUnitRepository.GetAllAsync(cancellationToken,
                    mu => mu.MagnitudeUnitName == EntityNamesByDefault.DefaultMagnitudeUnit)).FirstOrDefault()
                ?? throw new NotFoundException(nameof(MagnitudeUnit), $"With name {EntityNamesByDefault.DefaultMagnitudeUnit}");
            entity.MagnitudeUnits = new List<MagnitudeUnit>
            {
                undefinedMagnitudeUnit
            };

            await _eventCategoryRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
