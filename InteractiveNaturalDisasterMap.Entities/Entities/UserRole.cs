﻿namespace InteractiveNaturalDisasterMap.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string RoleName { get; set; } = null!;

        public ICollection<User> Users { get; set; } = null!;
    }
}
