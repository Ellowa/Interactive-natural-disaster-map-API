using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.SetModeratorPermission
{
    public class SetModeratorPermissionRequest : IRequest
    {
        public SetModeratorPermissionDto SetModeratorPermissionDto { get; set; } = null!;
    }
}
