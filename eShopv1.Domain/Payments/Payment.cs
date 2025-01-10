using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Orders;
using eShopv1.Domain.Payments.Events;

namespace eShopv1.Domain.Payments;

public sealed class Payment : AggregateRoot
{
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public PaymentSummary PaymentSummary { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string PaymentIntentId { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Payment(Guid id, Guid orderId, decimal amount, string currency, string paymentIntentId, DateTime paymentDate) : base(id)
    {
        OrderId = orderId;
        Amount = amount;
        Currency = currency;
        Status = PaymentStatus.Pending;
        PaymentIntentId = paymentIntentId;
        PaymentDate = paymentDate;
    }

    public static Payment Create(Guid id, Guid orderId, PaymentMethod paymentMethod, PaymentSummary paymentSummary, decimal amount, string currency, string paymentIntentId, DateTime paymentDate)
    {
        var payment = new Payment(id, orderId, amount, currency, paymentIntentId, paymentDate);
        payment.PaymentSummary = paymentSummary;
        payment.PaymentMethod = paymentMethod;

        payment.RaiseDomainEvent(new PaymentCreatedDomainEvent(id ,orderId, paymentMethod, paymentSummary, amount, currency, paymentIntentId, PaymentStatus.Pending, paymentDate));

        return payment;
    }
}