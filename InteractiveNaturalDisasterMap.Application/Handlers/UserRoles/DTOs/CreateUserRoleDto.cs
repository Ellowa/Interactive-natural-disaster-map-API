using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs
{
    public class CreateUserRoleDto
    {
        public string RoleName { get; set; } = null!;

        public UserRole Map()
        {
            UserRole userRole = new UserRole()
            {
                RoleName = this.RoleName,
            };
            return userRole;
        }
    }
}
