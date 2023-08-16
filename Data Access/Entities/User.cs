namespace Data_Access.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string Login { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public string JwtRefreshToken { get; set; } = null!;

        public int RoleId { get; set; }

        public UserRole Role { get; set; } = null!;

        public ICollection<EventsCollection> EventsCollection { get; set; } = null!;

        public ICollection<Event> Events { get; set; } = null!;
    }
}
