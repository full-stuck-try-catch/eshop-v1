using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Users
{
    public class Permission 
    {
        public static readonly Permission UsersRead = new(1, "users:read");
        public static readonly Permission UsersManage = new(2, "users:manage");

        private Permission(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; init; }

        public string Name { get; init; }
    }
}
