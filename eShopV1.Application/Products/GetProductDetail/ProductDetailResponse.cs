namespace eShopV1.Application.Products.GetProductDetail;

public sealed record ProductDetailResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string PictureUrl,
    string Brand,
    string Type,
    int QuantityInStock,
    DateTime CreatedAt,
    DateTime? UpdatedAt);