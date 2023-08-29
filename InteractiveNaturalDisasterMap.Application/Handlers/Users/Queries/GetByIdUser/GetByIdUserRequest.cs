
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByIdUser
{
    public class GetByIdUserRequest : IRequest<UserDto>
    {
        public GetByIdUserDto GetByIdUserDto { get; set; } = null!;
    }
}
