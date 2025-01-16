using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;

namespace eShopV1.Application.Carts.GetCart;

public sealed record GetCartQuery(Guid CartId) : ICachedQuery<ShoppingCartResponse>
{
    public string CacheKey => $"cart-{CartId}";

    public TimeSpan? Expiration => TimeSpan.FromDays(7);
}