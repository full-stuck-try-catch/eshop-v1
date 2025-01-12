namespace eShopV1.API.Controllers.ShoppingCarts;

public sealed record CreateOrUpdateCartRequest(
    Guid? CartId,
    Guid BuyerId,
    List<CartItemRequest> Items,
    CouponRequest? Coupon,
    string? ClientSecret,
    string? PaymentIntentId);

public sealed record CartItemRequest(
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