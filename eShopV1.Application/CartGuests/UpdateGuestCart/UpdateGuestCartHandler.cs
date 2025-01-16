using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;
using eShopv1.Domain.Abstractions;

namespace eShopV1.Application.CartGuests.GetGuestCart;

internal sealed class UpdateGuestCartHandler : IQueryHandler<UpdateGuestCartQuery, UpdateGuestCartResponse>
{
    private readonly ICacheService _cacheService;

    public UpdateGuestCartHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Result<UpdateGuestCartResponse>> Handle(
        UpdateGuestCartQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"guest_cart:{request.CartId}";

        var updateCart = UpdateGuestCartResponse.Create(request.CartId,
            request.Items.Select(item => new UpdateGuestCartResoneItem(item.ProductId,
            item.Quantity,
            item.ProductName,
            item.Price,
            item.PictureUrl,
            item.Currency,
            item.Brand,
            item.Type)).ToList());

        await _cacheService.SetAsync(cacheKey, updateCart, request.Expiration, cancellationToken);

        return Result.Success(updateCart);
    }
}
