using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent
{
    public class CreateNaturalDisasterEventRequest : IRequest<int>
    {
        public CreateNaturalDisasterEventDto CreateNaturalDisasterEventDto { get; set; } = null!;
        public string SourceName { get; set; } = string.Empty;
        public int? UserId { get; set; }
    }
}
