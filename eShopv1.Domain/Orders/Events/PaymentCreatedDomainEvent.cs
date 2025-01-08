using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders.Events;

public sealed record PaymentCreatedDomainEvent(Guid OrderId, Guid PaymentId, PaymentMethod PaymentMethod, decimal Amount, string Currency, string PaymentIntentId, PaymentStatus Status, DateTime PaymentDate) : IDomainEvent;  