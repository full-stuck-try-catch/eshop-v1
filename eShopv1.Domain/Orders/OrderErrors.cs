using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders;

public static class OrderErrors
{
    public static readonly Error OrderItemsRequired = new("Order.ItemsRequired", "Order items are required");
    public static readonly Error ShippingAddressRequired = new("Order.ShippingAddressRequired", "Shipping address is required");
    public static readonly Error PaymentSummaryRequired = new("Order.PaymentSummaryRequired", "Payment summary is required");
    public static readonly Error DeliveryMethodRequired = new("Order.DeliveryMethodRequired", "Delivery method is required");
    public static readonly Error SubtotalMustBeGreaterThanZero = new("Order.SubtotalMustBeGreaterThanZero", "Subtotal must be greater than zero");
    public static readonly Error DiscountMustBeGreaterThanZero = new("Order.DiscountMustBeGreaterThanZero", "Discount must be greater than zero");
    public static readonly Error OrderAlreadyExists = new("Order.AlreadyExists", "Order already exists");
}
