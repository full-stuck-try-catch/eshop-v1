using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;

namespace eShopV1.Application.ShoppingCarts.GetCart;

public sealed record GetCartByBuyerQuery(Guid BuyerId) : ICachedQuery<ShoppingCartResponse>
{
    public string CacheKey => $"cart-buyer-{BuyerId}";

    public TimeSpan? Expiration => TimeSpan.FromDays(7);
}