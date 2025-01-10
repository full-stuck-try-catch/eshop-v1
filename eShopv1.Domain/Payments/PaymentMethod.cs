using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Payments;

public enum PaymentMethod{
    CreditCard,
    DebitCard,
    BankTransfer,
    Cash,
    Other
}
