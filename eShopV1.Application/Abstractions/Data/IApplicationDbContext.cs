using eShopv1.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get;}

        DbSet<Role> Roles { get; }

        DbSet<Permission> Permissions { get; }
    }
}
