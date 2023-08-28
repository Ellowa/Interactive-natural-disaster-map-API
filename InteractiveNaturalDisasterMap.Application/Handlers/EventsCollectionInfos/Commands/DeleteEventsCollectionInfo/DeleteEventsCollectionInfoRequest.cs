using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.DeleteEventsCollectionInfo
{
    public class DeleteEventsCollectionInfoRequest : IRequest
    {
        public DeleteEventsCollectionInfoDto DeleteEventsCollectionInfoDto { get; set; } = null!;
    }
}
