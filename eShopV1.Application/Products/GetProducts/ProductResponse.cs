namespace eShopV1.Application.Products.GetProducts;

public sealed record ProductResponse(
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