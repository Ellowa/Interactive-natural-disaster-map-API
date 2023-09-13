using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs
{
    public class CreateEventsCollectionInfoDto
    {
        public string CollectionName { get; set; } = null!;

        public EventsCollectionInfo Map(int userId)
        {
            EventsCollectionInfo eventsCollectionInfo = new EventsCollectionInfo()
            {
                CollectionName = this.CollectionName,
                UserId = userId,
            };
            return eventsCollectionInfo;
        }
    }
}
