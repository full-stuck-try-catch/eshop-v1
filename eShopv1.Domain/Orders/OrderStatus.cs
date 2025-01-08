namespace eShopv1.Domain.Orders;
public enum OrderStatus
{
    Pending,
    PaymentReceived,
    PaymentFailed,
    PaymentMismatch,
    Refunded
}
