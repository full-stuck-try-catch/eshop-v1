using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Payments.Events;

public sealed record PaymentCreatedDomainEvent(Guid OrderId, Guid PaymentId, PaymentMethod PaymentMethod, PaymentSummary PaymentSummary, decimal Amount, string Currency, string PaymentIntentId, PaymentStatus Status, DateTime PaymentDate) : IDomainEvent;  