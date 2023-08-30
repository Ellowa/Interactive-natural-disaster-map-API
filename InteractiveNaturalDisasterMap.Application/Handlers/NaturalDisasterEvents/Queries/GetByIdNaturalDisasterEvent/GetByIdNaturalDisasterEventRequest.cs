using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetByIdNaturalDisasterEvent
{
    public class GetByIdNaturalDisasterEventRequest : IRequest<NaturalDisasterEventDto>
    {
        public GetByIdNaturalDisasterEventDto GetByIdNaturalDisasterEventDto { get; set; } = null!;
    }
}
