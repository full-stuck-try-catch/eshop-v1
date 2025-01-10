using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts;

public sealed record AppCoupon(
        string Name,
        decimal? AmountOff,
        decimal? PercentOff,
        string PromotionCode
    )
{
    public decimal CalculateDiscount(decimal subtotal)
    {
        if (subtotal <= 0) return 0;

        if (AmountOff is not null)
            return AmountOff.Value;

        if (PercentOff is not null)
            return subtotal * PercentOff.Value / 100;

        return 0;
    }
}
