using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;

namespace eShopV1.Application.Products.GetTypes;

public sealed record GetTypesCachedQuery() : ICachedQuery<List<string>>
{
    public string CacheKey => "product-types";

    public TimeSpan? Expiration => TimeSpan.FromHours(1);
}