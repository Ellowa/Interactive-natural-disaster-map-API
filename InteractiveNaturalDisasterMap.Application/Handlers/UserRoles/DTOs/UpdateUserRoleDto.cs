using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs
{
    public class UpdateUserRoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = null!;

        public UserRole Map()
        {
            UserRole userRole = new UserRole()
            {
                Id = this.Id,
                RoleName = this.RoleName,
            };
            return userRole;
        }
    }
}
