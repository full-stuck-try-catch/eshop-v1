namespace eShopV1.Application.Carts.CreateOrUpdateCart;

public sealed record CreateOrUpdateCartResponse(
    Guid CartId,
    Guid BuyerId,
    int ItemCount,
    decimal TotalPrice,
    bool IsNew,
    DateTime CreatedAt,
    DateTime? UpdatedAt);