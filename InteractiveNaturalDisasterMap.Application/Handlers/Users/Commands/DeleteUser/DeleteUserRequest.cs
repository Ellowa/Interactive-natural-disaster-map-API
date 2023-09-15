using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserRequest : IRequest
    {
        public DeleteUserDto DeleteUserDto { get; set; } = null!;
        public int UserId { get; set; }
    }
}
