using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Caching;
using eShopv1.Domain.Abstractions;
using eShopv1.Domain.ShoppingCarts;

namespace eShopV1.Application.Carts.DeleteCart;

internal sealed class DeleteCartCommandHandler : ICommandHandler<DeleteCartCommand>
{
    private readonly IShoppingCartRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public DeleteCartCommandHandler(
        IShoppingCartRepository repository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<Result> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _repository.GetByIdAsync(request.CartId, cancellationToken);

        if (cart is null)
        {
            return Result.Failure(ShoppingCartErrors.ShoppingCartNotFound);
        }

        var buyerId = cart.BuyerId;

        _repository.Remove(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Remove from cache
        await InvalidateCartCache(request.CartId, buyerId);

        return Result.Success();
    }

    private async Task InvalidateCartCache(Guid cartId, Guid buyerId)
    {
        var cartCacheKey = $"cart-{cartId}";
        var buyerCartCacheKey = $"cart-buyer-{buyerId}";

        await _cacheService.RemoveAsync(cartCacheKey);
        await _cacheService.RemoveAsync(buyerCartCacheKey);
    }
}