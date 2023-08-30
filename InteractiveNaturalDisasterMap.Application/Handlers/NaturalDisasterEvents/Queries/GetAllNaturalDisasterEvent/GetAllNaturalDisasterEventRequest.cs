using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetAllNaturalDisasterEvent
{
    public class GetAllNaturalDisasterEventRequest : IRequest<IList<NaturalDisasterEventDto>>
    {
        public GetAllNaturalDisasterEventDto GetAllNaturalDisasterEventDto { get; set; } = null!;
    }
}
