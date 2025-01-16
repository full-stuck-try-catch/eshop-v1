using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;

namespace eShopV1.Application.CartGuests.GetGuestCart;

public sealed record UpdateGuestCartQuery(string CartId, List<GuestCartItemQueryRequest> Items) : IQuery<UpdateGuestCartResponse>
{
    public string CacheKey => $"guest_cart:{CartId}";

    public TimeSpan? Expiration => TimeSpan.FromDays(3);
}

public sealed record GuestCartItemQueryRequest(
    Guid ProductId,
    int Quantity , string ProductName , decimal Price, string PictureUrl,string Currency, string Brand, string Type);