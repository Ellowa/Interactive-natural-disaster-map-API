using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetByIdInThirdPartyApiNaturalDisasterEvent
{
    public class GetByIdInThirdPartyApiNaturalDisasterEventRequest : IRequest<NaturalDisasterEventDto>
    {
        public GetByIdInThirdPartyApiNaturalDisasterEventDto GetByIdNaturalDisasterEventDto { get; set; } = null!;
    }
}
