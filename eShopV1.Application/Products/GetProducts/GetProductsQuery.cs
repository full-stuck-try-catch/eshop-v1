using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Pagination;

namespace eShopV1.Application.Products.GetProducts;

public sealed record GetProductsQuery(
    string? SearchTerm,
    string? Brand,
    decimal? MinPrice,
    decimal? MaxPrice,
    int? Status,
    string? SortBy,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<ProductResponse>>;