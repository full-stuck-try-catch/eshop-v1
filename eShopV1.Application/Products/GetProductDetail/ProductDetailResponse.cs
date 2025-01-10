namespace eShopV1.Application.Products.GetProductDetail;

public sealed record ProductDetailResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string PictureUrl,
    string Brand,
    int QuantityInStock,
    string Status,
    bool IsAvailable,
    DateTime CreatedAt,
    DateTime? UpdatedAt);