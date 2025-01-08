using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Orders.Events;

namespace eShopv1.Domain.Orders;

public sealed class Payment : Entity
{
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string PaymentIntentId { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime PaymentDate { get; private set; }

    private Payment() { }

    private Payment(Guid id, Guid orderId, PaymentMethod paymentMethod, decimal amount, string currency, string paymentIntentId, PaymentStatus status, DateTime paymentDate) : base(id)
    {
        OrderId = orderId;
        PaymentMethod = paymentMethod;
        Amount = amount;
        Currency = currency;
        PaymentIntentId = paymentIntentId;
    }

    public static Payment Create(Guid id, Guid orderId, PaymentMethod paymentMethod, decimal amount, string currency, string paymentIntentId, PaymentStatus status, DateTime paymentDate)
    {
        var payment = new Payment(id, orderId, paymentMethod, amount, currency, paymentIntentId, status, paymentDate);

        payment.UpdateStatus(status);

        payment.RaiseDomainEvent(new PaymentCreatedDomainEvent(orderId, id, paymentMethod, amount, currency, paymentIntentId, status, paymentDate));

        return payment;
    }

    public void UpdateStatus(PaymentStatus status)
    {
        Status = status;
    }   
}