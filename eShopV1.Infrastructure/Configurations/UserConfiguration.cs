using eShopv1.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopV1.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(u => u.FirstName)
               .HasMaxLength(200)
               .IsRequired(false);

            builder.Property(u => u.LastName)
               .HasMaxLength(200)
               .IsRequired(false);

            builder.Property(u => u.Email)
               .HasMaxLength(200)
               .IsRequired(true);

            builder.Property(u => u.PasswordHash).IsRequired();

            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.OwnsOne(u => u.Address, address =>
            {
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