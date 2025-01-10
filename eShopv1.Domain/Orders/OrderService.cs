using eShopv1.Domain.ShoppingCarts;

namespace eShopv1.Domain.Orders;

public sealed class OrderService{
    public OrderPriceDetail CalculateOrderPrice(List<OrderItem> items, AppCoupon? coupon){
        if(coupon is null){
            throw new ArgumentNullException(nameof(coupon));
        }

        decimal subtotal = items.Sum(item => item.Price * item.Quantity);
        decimal discount = coupon.CalculateDiscount(subtotal);
        return new OrderPriceDetail(subtotal, discount);
    }
}