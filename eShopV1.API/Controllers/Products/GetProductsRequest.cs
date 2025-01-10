namespace eShopV1.API.Controllers.Products;

public sealed record GetProductsRequest(
    string? SearchTerm = null,
    string? Brand = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    int? Status = null,
    string? SortBy = null,
    string? SortOrder = null,
    int Page = 1,
    int PageSize = 10);