namespace eShopv1.Domain.Payments;

public enum PaymentStatus{
    Pending,
    Succeeded,
    Failed,
    Refunded,
    PartiallyRefunded,
    PartiallySucceeded,
    PartiallyFailed,
}