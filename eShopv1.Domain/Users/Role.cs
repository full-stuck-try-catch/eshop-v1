using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Users
{
    public sealed class Role 
    {
        public static readonly Role Admin = new(1, "Admin");
        public static readonly Role Registered = new(2, "Registered");

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; init; }
        public string Name { get; init; }

        public ICollection<UserRole> UserRoles { get; init; } = new List<UserRole>();

        public ICollection<User> Users { get; init; } = new List<User>();

        public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
    }
}
