using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserRequest : IRequest<string>
    {
        public CreateUserDto CreateUserDto { get; set; } = null!;
    }
}
