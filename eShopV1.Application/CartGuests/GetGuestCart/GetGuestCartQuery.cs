using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.CartGuests.GetGuestCart;

public sealed record GetGuestCartQuery(string CartId) : IQuery<UpdateGuestCartResponse>
{
    public string CacheKey => $"guest_cart:{CartId}";
}