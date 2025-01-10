using eShopv1.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopV1.Infrastructure.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BuyerId)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired(false);

        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(c => c.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.ClientSecret).IsRequired(false);

        builder.Property(c => c.PaymentIntentId).IsRequired(false);

        builder.OwnsOne(c => c.Coupon, coupon =>
        {
            coupon.Property(c => c.PromotionCode)
                .HasMaxLength(50);
            coupon.Property(c => c.Name).HasMaxLength(200);
            coupon.Property(c => c.AmountOff).HasColumnType("decimal(18,2)").IsRequired(false);
            coupon.Property(c => c.PercentOff).HasColumnType("decimal(18,2)").IsRequired(false);
        });

        builder.Ignore(c => c.TotalCartPrice);
    }
}
