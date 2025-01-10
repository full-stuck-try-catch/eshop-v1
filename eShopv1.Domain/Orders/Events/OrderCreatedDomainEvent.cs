using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders.Events
{
    public sealed record OrderCreatedDomainEvent(Guid Id , Guid UserId) : IDomainEvent;
}
