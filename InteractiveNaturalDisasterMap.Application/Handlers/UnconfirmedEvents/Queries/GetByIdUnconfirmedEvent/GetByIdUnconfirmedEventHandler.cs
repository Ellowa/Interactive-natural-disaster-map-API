using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetByIdUnconfirmedEvent
{
    public class GetByIdUnconfirmedEventHandler : IRequestHandler<GetByIdUnconfirmedEventRequest, UnconfirmedEventDto>
    {
        private readonly IUnconfirmedEventRepository _unconfirmedEventRepository;

        public GetByIdUnconfirmedEventHandler(IUnitOfWork unitOfWork)
        {
            _unconfirmedEventRepository = unitOfWork.UnconfirmedEventRepository;
        }

        public async Task<UnconfirmedEventDto> Handle(GetByIdUnconfirmedEventRequest request,
            CancellationToken cancellationToken)
        {
            var unconfirmedEvent = await _unconfirmedEventRepository.GetByEventId(
                                       request.GetByIdUnconfirmedEventDto.EventId, cancellationToken, 
                                       ue => ue.User.Role,
                                       ue => ue.Event.Category,
                                       ue => ue.Event.Source,
                                       ue => ue.Event.MagnitudeUnit,
                                       ue => ue.Event.EventHazardUnit)
                                   ?? throw new NotFoundException(nameof(UnconfirmedEvent),
                                       request.GetByIdUnconfirmedEventDto.EventId);

            return new UnconfirmedEventDto(unconfirmedEvent);
        }
    }
}
