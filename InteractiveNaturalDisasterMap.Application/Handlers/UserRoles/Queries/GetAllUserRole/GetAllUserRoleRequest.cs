using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetAllUserRole
{
    public class GetAllUserRoleRequest : IRequest<IList<UserRoleDto>>
    {
    }
}
