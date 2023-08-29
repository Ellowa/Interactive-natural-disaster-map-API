using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetAllUnconfirmedEvent
{
    public class GetAllUnconfirmedEventRequest : IRequest<IList<UnconfirmedEventDto>>
    {
    }
}
