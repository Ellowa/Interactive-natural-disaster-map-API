using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs
{
    public class EventsCollectionInfoDto
    {
        public int Id { get; set; }
        public string CollectionName { get; set; }
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public List<NaturalDisasterEventDto> EventDtos { get; set; }

        public EventsCollectionInfoDto(EventsCollectionInfo eventsCollectionInfo)
        {
            Id = eventsCollectionInfo.Id;
            CollectionName = eventsCollectionInfo.CollectionName;
            UserId = eventsCollectionInfo.UserId;
            UserLogin = eventsCollectionInfo.User.Login;
            EventDtos = new List<NaturalDisasterEventDto>();
            if (eventsCollectionInfo.EventsCollection?.Any() == true)
            {
                foreach (var naturalDisasterEvent in eventsCollectionInfo.EventsCollection.Select(ec => ec.Event))
                {
                    EventDtos.Add(new NaturalDisasterEventDto(naturalDisasterEvent));
                }
            }
        }
    }
}
