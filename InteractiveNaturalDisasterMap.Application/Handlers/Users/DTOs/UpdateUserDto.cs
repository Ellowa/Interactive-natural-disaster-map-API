using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Telegram { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public User Map(byte[] passwordHash, byte[] passwordSalt, int roleId)
        {
            User user = new User()
            {
                Id = Id,
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                LastName = this.LastName,
                Email = this.Email,
                Telegram = this.Telegram,
                Login = this.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = roleId,
            };
            return user;
        }
    }
}
