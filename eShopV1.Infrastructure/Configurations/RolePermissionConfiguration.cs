using eShopv1.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Infrastructure.Configurations
{
    internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("role_permissions");

            builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });

            builder.HasData( 
                new RolePermission
                {
                    RoleId = Role.User.Id,
                    PermissionId = Permission.UsersRead.Id
                },
                new RolePermission
                {
                    RoleId = Role.Admin.Id,
                    PermissionId = Permission.UsersRead.Id
                },
                new RolePermission
                {
                    RoleId = Role.Admin.Id,
                    PermissionId = Permission.UsersManage.Id
                });
        }
    }
}
