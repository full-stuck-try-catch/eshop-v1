namespace eShopV1.Application.Products.GetProducts;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string PictureUrl,
    string Brand,
    int QuantityInStock,
    int Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);