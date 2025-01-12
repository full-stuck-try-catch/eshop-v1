namespace eShopV1.Application.ShoppingCarts.GetCart;

public sealed record ShoppingCartResponse(
    Guid Id,
    Guid BuyerId,
    List<CartItemResponse> Items,
    CouponResponse? Coupon,
    string? ClientSecret,
    string? PaymentIntentId,
    decimal TotalPrice,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public sealed record CartItemResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    string PictureUrl,
    string Currency,
    decimal LineTotal);

public sealed record CouponResponse(
    string Name,
    decimal? AmountOff,
    decimal? PercentOff,
    string PromotionCode,
    decimal DiscountAmount);