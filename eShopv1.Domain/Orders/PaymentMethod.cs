using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders;

public enum PaymentMethod{
    CreditCard,
    DebitCard,
    BankTransfer,
    Cash,
    Other
}
