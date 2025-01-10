using eShopv1.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oi => oi.ShortName).HasMaxLength(100).IsRequired();

        builder.Property(oi => oi.DeliveryTime).HasMaxLength(50).IsRequired();

        builder.Property(oi => oi.Description).HasMaxLength(500).IsRequired();
    }
}
