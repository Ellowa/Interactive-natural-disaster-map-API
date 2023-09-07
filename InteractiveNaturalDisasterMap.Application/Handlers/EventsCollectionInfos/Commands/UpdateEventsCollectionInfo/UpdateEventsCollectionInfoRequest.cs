using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.UpdateEventsCollectionInfo
{
    public class UpdateEventsCollectionInfoRequest : IRequest
    {
        public UpdateEventsCollectionInfoDto UpdateEventsCollectionInfoDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
