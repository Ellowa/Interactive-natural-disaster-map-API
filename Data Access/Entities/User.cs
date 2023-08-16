namespace Data_Access.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string Login { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string JwtRefreshToken { get; set; }

        public int RoleId { get; set; }

        public UserRole Role { get; set; }

        public ICollection<EventsCollection> EventsCollection { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
