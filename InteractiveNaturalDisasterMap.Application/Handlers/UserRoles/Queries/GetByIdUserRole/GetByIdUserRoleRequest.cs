using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetByIdUserRole
{
    public class GetByIdUserRoleRequest : IRequest<UserRoleDto>
    {
        public GetByIdUserRoleDto GetByIdUserRoleDto { get; set; } = null!;
    }
}
