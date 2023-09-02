﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetByUserIdUnconfirmedEvent
{
    public class GetByUserIdUnconfirmedEventHandler : IRequestHandler<GetByUserIdUnconfirmedEventRequest, IList<UnconfirmedEventDto>>
    {
        private readonly IUnconfirmedEventRepository _unconfirmedEventRepository;

        public GetByUserIdUnconfirmedEventHandler(IUnitOfWork unitOfWork)
        {
            _unconfirmedEventRepository = unitOfWork.UnconfirmedEventRepository;
        }

        public async Task<IList<UnconfirmedEventDto>> Handle(GetByUserIdUnconfirmedEventRequest request,
            CancellationToken cancellationToken)
        {
            var unconfirmedEvents = await _unconfirmedEventRepository.GetAllAsync(cancellationToken,
                ue => ue.UserId == request.GetByUserIdUnconfirmedEventDto.UserId, 
                ue => ue.User.Role,
                ue => ue.Event.Category,
                ue => ue.Event.Source,
                ue => ue.Event.MagnitudeUnit,
                ue => ue.Event.EventHazardUnit);

            if (unconfirmedEvents.Count == 0)
                throw new NotFoundException(nameof(UnconfirmedEvent), request.GetByUserIdUnconfirmedEventDto.UserId);

            IList<UnconfirmedEventDto> unconfirmedEventDtos = new List<UnconfirmedEventDto>();
            foreach (var unconfirmedEvent in unconfirmedEvents)
            {
                unconfirmedEventDtos.Add(new UnconfirmedEventDto(unconfirmedEvent));
            }

            return unconfirmedEventDtos;
        }
    }
}
