namespace eShopv1.Domain.Orders;

public enum PaymentStatus{
    Pending,
    Succeeded,
    Failed,
    Refunded,
    PartiallyRefunded,
    PartiallySucceeded,
    PartiallyFailed,
}