using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public UserRoleDto(UserRole userRole)
        {
            Id = userRole.Id;
            RoleName = userRole.RoleName;
        }
    }
}
