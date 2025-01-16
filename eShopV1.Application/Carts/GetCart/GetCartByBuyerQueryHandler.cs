using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using eShopv1.Domain.Abstractions;
using eShopv1.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Carts.GetCart;

internal sealed class GetCartByBuyerQueryHandler 
    : IQueryHandler<GetCartByBuyerQuery, ShoppingCartResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCartByBuyerQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ShoppingCartResponse>> Handle(
        GetCartByBuyerQuery request, 
        CancellationToken cancellationToken)
    {
        var cart = await _context.ShoppingCarts
            .Include(c => c.Items)
            .Where(c => c.BuyerId == request.BuyerId)
            .Select(c => new ShoppingCartResponse(
                c.Id,
                c.BuyerId,
                c.Items.Select(i => new CartItemResponse(
                    i.Id,
                    i.ProductId,
                    i.ProductName,
                    i.Price,
                    i.Quantity,
                    i.PictureUrl,
                    i.Currency,
                    i.Price * i.Quantity
                )).ToList(),
                c.Coupon != null ? new CouponResponse(
                    c.Coupon.Name,
                    c.Coupon.AmountOff,
                    c.Coupon.PercentOff,
                    c.Coupon.PromotionCode,
                    c.Coupon.CalculateDiscount(c.Items.Sum(i => i.Price * i.Quantity))
                ) : null,
                c.ClientSecret,
                c.PaymentIntentId,
                c.TotalCartPrice,
                c.CreatedAt,
                c.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return Result.Failure<ShoppingCartResponse>(ShoppingCartErrors.ShoppingCartNotFound);
        }

        return Result.Success(cart);
    }
}