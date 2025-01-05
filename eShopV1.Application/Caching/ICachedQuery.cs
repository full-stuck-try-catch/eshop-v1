using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Caching
{
    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

    public interface ICachedQuery
    {
        string CacheKey { get; }

        TimeSpan? Expiration { get; }
    }
}
