using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.ShoppingCarts.CreateOrUpdateCart;

public sealed record CreateOrUpdateCartCommand(
    Guid? CartId,
    Guid BuyerId,
    List<CreateCartItemRequest> Items,
    CouponRequest? Coupon,
    string? ClientSecret,
    string? PaymentIntentId) : ICommand<CreateOrUpdateCartResponse>;

public sealed record CreateCartItemRequest(
    Guid ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    string PictureUrl,
    string Currency);

public sealed record CouponRequest(
    string Name,
    decimal? AmountOff,
    decimal? PercentOff,
    string PromotionCode);