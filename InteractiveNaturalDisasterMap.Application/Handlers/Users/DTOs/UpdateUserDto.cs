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
    }
}
