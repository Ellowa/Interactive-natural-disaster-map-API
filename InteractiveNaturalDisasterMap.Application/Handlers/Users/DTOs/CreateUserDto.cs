using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs
{
    public class CreateUserDto
    {
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Telegram { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public User Map(byte[] passwordHash, byte[] passwordSalt, int roleId)
        {
            User user = new User()
            {
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                LastName = this.LastName,
                Email = this.Email,
                Telegram = this.Telegram,
                Login = this.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                JwtRefreshToken = this.RefreshToken,
                RoleId = roleId,
            };
            return user;
        }
    }
}
