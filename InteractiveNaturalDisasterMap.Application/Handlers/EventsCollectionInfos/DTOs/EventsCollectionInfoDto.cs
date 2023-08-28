
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs
{
    public class EventsCollectionInfoDto
    {
        public int Id { get; set; }
        public string CollectionName { get; set; }
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        //public List<Event> EventDtos { get; set; }

        public EventsCollectionInfoDto(EventsCollectionInfo eventsCollectionInfo)
        {
            Id = eventsCollectionInfo.Id;
            CollectionName = eventsCollectionInfo.CollectionName;
            UserId = eventsCollectionInfo.UserId;
            UserLogin = eventsCollectionInfo.User.Login;
            //Todo EventDtos
            throw new NotImplementedException();
        }
    }
}
