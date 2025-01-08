using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts;

public sealed class AppCoupon : Entity
{
    public string Name { get; private set; }
    public decimal? AmountOff { get; private set; }
    public decimal? PercentOff { get; private set; }
    public string PromotionCode { get; private set; }
    public string CouponId { get; private set; }

    private AppCoupon() { }

    private AppCoupon(Guid id, string name, decimal? amountOff, decimal? percentOff, string promotionCode, string couponId) : base(id)
    {
        Name = name;
        AmountOff = amountOff;
        PercentOff = percentOff;
        PromotionCode = promotionCode;
        CouponId = couponId;
    }

     public decimal CalculateDiscount(decimal subtotal){
        if(subtotal <= 0){
            return 0;
        }
       

        if(AmountOff is not null){
            return AmountOff.Value;
        }

        if(PercentOff is not null){  
            return subtotal * PercentOff.Value / 100;
        }

        return 0;
    }
}
