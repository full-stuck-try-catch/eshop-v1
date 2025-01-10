using eShopv1.Domain.Abstractions;
using eShopv1.Domain.ShoppingCarts.Events;

namespace eShopv1.Domain.ShoppingCarts;

public sealed class ShoppingCart : AggregateRoot
{
    private readonly List<CartItem> _items = new List<CartItem>();
    public Guid BuyerId { get; private set; }
    public IReadOnlyList<CartItem> Items => _items.ToList();
    public AppCoupon? Coupon { get; private set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    private ShoppingCart() : base(Guid.NewGuid())
    {
    }

    private ShoppingCart(Guid id, Guid buyerId, AppCoupon? coupon, string? clientSecret, string? paymentIntentId, DateTime createdAt) : base(id)
    {
        BuyerId = buyerId;
        Coupon = coupon;
        ClientSecret = clientSecret;
        PaymentIntentId = paymentIntentId;
        CreatedAt = createdAt;
    }

    public static ShoppingCart Create(Guid id, Guid buyerId, AppCoupon? coupon , List<CartItem> items, string? clientSecret, string? paymentIntentId, DateTime createdAt)
    {
        var shoppingCart = new ShoppingCart(id, buyerId, coupon, clientSecret, paymentIntentId, createdAt);
        
        shoppingCart._items.AddRange(items);

        shoppingCart.RaiseDomainEvent(new ShoppingCartCreatedEvent(shoppingCart.Id, shoppingCart.BuyerId, shoppingCart._items, shoppingCart.Coupon, shoppingCart.CreatedAt));

        return shoppingCart;
    }

    public Result AddItem(CartItem item)
    {
        if(item.Quantity <= 0){
            return Result.Failure(ShoppingCartErrors.InvalidQuantity);
        }

        if(_items.Any(i => i.ProductId == item.ProductId)){
            return Result.Failure(ShoppingCartErrors.ItemAlreadyExists);
        }

        _items.Add(item);
        return Result.Success();
    }

    public Result RemoveItem(CartItem item)
    {
        if(!_items.Any(i => i.ProductId == item.ProductId)){
            return Result.Failure(ShoppingCartErrors.ItemNotFound);
        }

        _items.Remove(item);
        return Result.Success();
    }

    public Result ApplyCoupon(AppCoupon coupon)
    {
        if(Coupon is null){
            return Result.Failure(ShoppingCartErrors.CouponNotFound);
        }

        Coupon = coupon;

        return Result.Success();
    }

    public decimal TotalCartPrice => _items.Sum(item => item.Price * item.Quantity);
}