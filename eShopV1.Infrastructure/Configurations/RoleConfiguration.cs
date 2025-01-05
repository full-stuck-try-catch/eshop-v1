using eShopv1.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopV1.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(role => role.Id);

            builder.HasMany(role => role.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasData(new[] { Role.Admin, Role.User });
        }
    }
}
