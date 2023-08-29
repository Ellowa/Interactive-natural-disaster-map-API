using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole
{
    public class CreateUserRoleRequest : IRequest<int>
    {
        public CreateUserRoleDto CreateUserRoleDto { get; set; } = null!;
    }
}
