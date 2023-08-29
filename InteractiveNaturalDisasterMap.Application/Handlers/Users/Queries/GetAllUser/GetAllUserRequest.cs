using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetAllUser
{
    public class GetAllUserRequest : IRequest<IList<UserDto>>
    {
    }
}
