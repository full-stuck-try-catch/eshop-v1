using eShopv1.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .HasColumnType("int")
            .IsRequired();

        builder.OwnsOne(oi => oi.ProductItemOrdered, pio =>
        {
            pio.Property(p => p.ProductId).IsRequired();
            pio.Property(p => p.ProductName).HasMaxLength(100).IsRequired();
            pio.Property(p => p.PictureUrl).HasMaxLength(500).IsRequired();
        });
    }
}
