using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts.Events;

public sealed record ShoppingCartCreatedEvent(
    Guid Id,
    Guid UserId,
    List<CartItem> Items,
    AppCoupon? Coupon,
    DateTime CreatedAt
) : IDomainEvent;