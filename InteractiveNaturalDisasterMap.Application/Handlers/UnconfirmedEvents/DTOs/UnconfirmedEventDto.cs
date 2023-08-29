
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs
{
    public class UnconfirmedEventDto
    {
        public EventDto EventDto { get; set; }
        public UserDto UserDto { get; set; }

        public UnconfirmedEventDto(UnconfirmedEvent unconfirmedEvent)
        {
            //Todo EventDto
            UserDto = new UserDto(unconfirmedEvent.User);
            EventDto = new EventDto(unconfirmedEvent.Event);
        }
    }
}
