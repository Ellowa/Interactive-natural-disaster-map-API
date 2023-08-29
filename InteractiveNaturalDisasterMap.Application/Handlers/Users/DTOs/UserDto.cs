using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Telegram { get; set; }

        public string Login { get; set; }

        public string RoleName { get; set; }

        public UserDto(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            LastName = user.LastName;
            Email = user.Email;
            Telegram = user.Telegram;
            Login = user.Login;
            RoleName = user.Role.RoleName;
        }
    }
}
