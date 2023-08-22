﻿namespace InteractiveNaturalDisasterMap.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; init; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string Login { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public string JwtRefreshToken { get; set; } = null!;

        public int RoleId { get; set; }

        public UserRole Role { get; set; } = null!;

        public ICollection<EventsCollectionInfo> EventsCollectionInfos { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;
    }
}