using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.UpdateUserRole
{
    public class UpdateUserRoleRequest : IRequest
    {
        public UpdateUserRoleDto UpdateUserRoleDto { get; set; } = null!;
    }
}
