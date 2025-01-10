using eShopv1.Domain.Abstractions;
using eShopv1.Domain.ShoppingCarts;

namespace eShopv1.Domain.Orders.Events;

public sealed record OrderUpdatedDomainEvent(
    Guid Id,
    List<OrderItem> Items,
    ShippingAddress ShippingAddress,
    AppCoupon? AppliedCoupon,
    DateTime? UpdatedAt
) : IDomainEvent;