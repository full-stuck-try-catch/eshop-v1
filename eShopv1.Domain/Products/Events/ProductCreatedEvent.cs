using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Products.Events;

public sealed record ProductCreatedEvent(Guid ProductId, string Name, string Brand, int QuantityInStock) : IDomainEvent;

