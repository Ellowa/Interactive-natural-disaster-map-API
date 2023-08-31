using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs
{
    public class UnconfirmedEventDto
    {
        public NaturalDisasterEventDto EventDto { get; set; }
        public UserDto UserDto { get; set; }

        public UnconfirmedEventDto(UnconfirmedEvent unconfirmedEvent)
        {
            UserDto = new UserDto(unconfirmedEvent.User);
            EventDto = new NaturalDisasterEventDto(unconfirmedEvent.Event);
        }
    }
}
