
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs
{
    public class UpdateEventsCollectionInfoDto
    {
        public int Id { get; set; }
        public string CollectionName { get; set; } = null!;

        public EventsCollectionInfo Map()
        {
            EventsCollectionInfo eventsCollectionInfo = new EventsCollectionInfo()
            {
                Id = this.Id,
                CollectionName = this.CollectionName,
            };
            return eventsCollectionInfo;
        }
    }
}
