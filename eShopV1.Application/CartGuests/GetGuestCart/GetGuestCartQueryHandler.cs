using eShopv1.Domain.Abstractions;
using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;

namespace eShopV1.Application.CartGuests.GetGuestCart;

internal sealed class GetGuestCartQueryHandler : IQueryHandler<GetGuestCartQuery, UpdateGuestCartResponse>
{
    private readonly ICacheService _cacheService;

    public GetGuestCartQueryHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Result<UpdateGuestCartResponse>> Handle(
        GetGuestCartQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = request.CacheKey;

        var guestCart = await _cacheService.GetAsync<UpdateGuestCartResponse>(cacheKey, cancellationToken);

        if (guestCart == null)
        {
            // Return empty cart if not found in cache
            return Result.Success(UpdateGuestCartResponse.Create(request.CartId, new List<UpdateGuestCartResoneItem>()));
        }

        return Result.Success(guestCart);
    }
}