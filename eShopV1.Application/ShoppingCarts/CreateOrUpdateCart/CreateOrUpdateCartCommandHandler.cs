using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopV1.Application.Abstractions.Time;
using eShopV1.Application.Caching;
using eShopv1.Domain.Abstractions;
using eShopv1.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.ShoppingCarts.CreateOrUpdateCart;

internal sealed class CreateOrUpdateCartCommandHandler 
    : ICommandHandler<CreateOrUpdateCartCommand, CreateOrUpdateCartResponse>
{
    private readonly IShoppingCartRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICacheService _cacheService;

    public CreateOrUpdateCartCommandHandler(
        IShoppingCartRepository repository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        ICacheService cacheService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _cacheService = cacheService;
    }

    public async Task<Result<CreateOrUpdateCartResponse>> Handle(
        CreateOrUpdateCartCommand request, 
        CancellationToken cancellationToken)
    {
        var currentTime = _dateTimeProvider.UtcNow;
        ShoppingCart? existingCart = null;
        bool isNew = true;

        // Try to get existing cart
        if (request.CartId.HasValue)
        {
            existingCart = await _repository.GetByIdAsync(request.CartId.Value, cancellationToken);
        }
        else
        {
            existingCart = await _repository.GetCartByBuyerIdAsync(request.BuyerId, cancellationToken);
        }

        if (existingCart is not null)
        {
            isNew = false;
             var resultUpdateCart = UpdateExistingCart(existingCart, request, currentTime);

            if(resultUpdateCart.IsFailure)
            {
                return Result.Failure<CreateOrUpdateCartResponse>(resultUpdateCart.Error);
            }
        }
        else
        {
            existingCart = await CreateNewCart(request, currentTime);
            _repository.Add(existingCart);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Update cache
        await InvalidateCartCache(existingCart.Id, existingCart.BuyerId);

        var response = new CreateOrUpdateCartResponse(
            existingCart.Id,
            existingCart.BuyerId,
            existingCart.Items.Count,
            existingCart.TotalCartPrice,
            isNew,
            existingCart.CreatedAt,
            existingCart.UpdatedAt);

        return Result.Success(response);
    }

    private async Task<ShoppingCart> CreateNewCart(
        CreateOrUpdateCartCommand request, 
        DateTime currentTime)
    {
        var cartId = request.CartId ?? Guid.NewGuid();
        
        var coupon = request.Coupon is not null 
            ? new AppCoupon(
                request.Coupon.Name,
                request.Coupon.AmountOff,
                request.Coupon.PercentOff,
                request.Coupon.PromotionCode)
            : null;

        var cartItems = request.Items.Select(item =>
            CartItem.Create(
                Guid.NewGuid(),
                cartId,
                item.ProductId,
                item.ProductName,
                item.Price,
                item.Quantity,
                item.PictureUrl,
                item.Currency)).ToList();

        return ShoppingCart.Create(
            cartId,
            request.BuyerId,
            coupon,
            cartItems,
            request.ClientSecret,
            request.PaymentIntentId,
            currentTime);
    }

    private Result UpdateExistingCart(
        ShoppingCart existingCart,
        CreateOrUpdateCartCommand request,
        DateTime currentTime)
    {

        existingCart.ClearCart();

        // Add new items
        foreach (var itemRequest in request.Items)
        {
            var cartItem = CartItem.Create(
                Guid.NewGuid(),
                existingCart.Id,
                itemRequest.ProductId,
                itemRequest.ProductName,
                itemRequest.Price,
                itemRequest.Quantity,
                itemRequest.PictureUrl,
                itemRequest.Currency);

            var addResult = existingCart.AddItem(cartItem);

            if (addResult.IsFailure)
            {
                return addResult;
            }
        }

        // Update coupon if provided
        if (request.Coupon is not null)
        {
            var coupon = new AppCoupon(
                request.Coupon.Name,
                request.Coupon.AmountOff,
                request.Coupon.PercentOff,
                request.Coupon.PromotionCode);

           var applyCouponResult = existingCart.ApplyCoupon(coupon);

            if (applyCouponResult.IsFailure)
            {
                return applyCouponResult;
            }
        }

        // Update payment details
        existingCart.ClientSecret = request.ClientSecret;
        existingCart.PaymentIntentId = request.PaymentIntentId;

        _repository.Update(existingCart);

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