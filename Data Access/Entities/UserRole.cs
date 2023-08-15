namespace Data_Access.Entities
{
    internal class UserRole : BaseEntity
    {
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
