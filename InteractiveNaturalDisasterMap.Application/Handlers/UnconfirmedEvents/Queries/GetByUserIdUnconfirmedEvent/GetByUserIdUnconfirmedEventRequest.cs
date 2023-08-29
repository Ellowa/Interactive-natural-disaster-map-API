using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetByUserIdUnconfirmedEvent
{
    public class GetByUserIdUnconfirmedEventRequest : IRequest<IList<UnconfirmedEventDto>>
    {
        public GetByUserIdUnconfirmedEventDto GetByUserIdUnconfirmedEventDto { get; set; } = null!;
    }
}
