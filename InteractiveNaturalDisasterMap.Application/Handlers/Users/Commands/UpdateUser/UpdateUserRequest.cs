using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserRequest : IRequest
    {
        public UpdateUserDto UpdateUserDto { get; set; } = null!;
    }
}
