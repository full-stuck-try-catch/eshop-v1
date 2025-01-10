using eShopv1.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopV1.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.Subtotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.Discount)
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.Currency)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(o => o.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.DeliveryMethod)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(o => o.AppliedCoupon, ac =>
        {
            ac.Property(c => c.Name).HasMaxLength(200).IsRequired();
            ac.Property(c => c.AmountOff).HasColumnType("decimal(18,2)");
            ac.Property(c => c.PercentOff).HasColumnType("decimal(18,2)");
            ac.Property(c => c.PromotionCode).HasMaxLength(50).IsRequired();
        });

        builder.OwnsOne(o => o.ShippingAddress, sa =>
        {
            sa.Property(a => a.Name).HasMaxLength(200).IsRequired();
            sa.Property(a => a.Line1).HasMaxLength(200).IsRequired();
            sa.Property(a => a.Line2).HasMaxLength(200);
            sa.Property(a => a.City).HasMaxLength(100).IsRequired();
            sa.Property(a => a.State).HasMaxLength(100).IsRequired();
            sa.Property(a => a.PostalCode).HasMaxLength(20).IsRequired();
            sa.Property(a => a.Country).HasMaxLength(100).IsRequired();
        });
    }
}
