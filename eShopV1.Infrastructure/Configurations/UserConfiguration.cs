using eShopv1.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopV1.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(u => u.Address, address =>
            {
                address.Property(a => a.Name).HasMaxLength(128);
                address.Property(a => a.Line1).HasMaxLength(256);
                address.Property(a => a.Line2).HasMaxLength(256);
                address.Property(a => a.City).HasMaxLength(128);
                address.Property(a => a.State).HasMaxLength(64);
                address.Property(a => a.PostalCode).HasMaxLength(32);
                address.Property(a => a.Country).HasMaxLength(64);
            });
        }
    }
}