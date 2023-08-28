using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs
{
    public class CreateEventsCollectionInfoDto
    {
        public string CollectionName { get; set; } = null!;

        public int UserId { get; set; }

        public EventsCollectionInfo Map()
        {
            EventsCollectionInfo eventsCollectionInfo = new EventsCollectionInfo()
            {
                CollectionName = this.CollectionName,
                UserId = this.UserId,
            };
            return eventsCollectionInfo;
        }
    }
}
