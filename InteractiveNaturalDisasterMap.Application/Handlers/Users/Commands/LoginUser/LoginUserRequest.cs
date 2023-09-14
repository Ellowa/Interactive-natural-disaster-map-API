using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.LoginUser
{
    public class LoginUserRequest : IRequest<string>
    {
        public LoginUserDto LoginUserDto { get; set; } = null!;
    }
}
