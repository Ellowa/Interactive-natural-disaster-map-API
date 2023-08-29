using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.DeleteUserRole
{
    public class DeleteUserRoleRequest : IRequest
    {
        public DeleteUserRoleDto DeleteUserRoleDto { get; set; } = null!;
    }
}
