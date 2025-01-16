using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;

namespace eShopV1.Application.Products.GetBrands;

public sealed record GetBrandsCachedQuery() : ICachedQuery<List<string>>
{
    public string CacheKey => "product-brands";

    public TimeSpan? Expiration => TimeSpan.FromHours(1);
}