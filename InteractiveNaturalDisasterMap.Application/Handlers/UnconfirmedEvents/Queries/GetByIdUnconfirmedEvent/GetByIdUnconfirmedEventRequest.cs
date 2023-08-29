using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetByIdUnconfirmedEvent
{
    public class GetByIdUnconfirmedEventRequest : IRequest<UnconfirmedEventDto>
    {
        public GetByIdUnconfirmedEventDto GetByIdUnconfirmedEventDto { get; set; } = null!;
    }
}
