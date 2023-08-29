using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByLoginUser
{
    public class GetByLoginUserRequest : IRequest<UserDto>
    {
        public GetByLoginUserDto GetByLoginUserDto { get; set; } = null!;
    }
}
