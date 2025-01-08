using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts;

public sealed class ShoppingCart : Entity
{
    private readonly List<CartItem> _items = new List<CartItem>();
    public Guid BuyerId { get; private set; }
    public IReadOnlyList<CartItem> Items => _items.ToList();
    public AppCoupon? Coupon { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private ShoppingCart() { }

    private ShoppingCart(Guid id, Guid buyerId, AppCoupon? coupon) : base(id)
    {
        BuyerId = buyerId;
        Coupon = coupon;
    }

    public static ShoppingCart Create(Guid id, Guid buyerId, AppCoupon? coupon , List<CartItem> items)
    {
        var shoppingCart = new ShoppingCart(id, buyerId, coupon);
        shoppingCart._items.AddRange(items);
        return shoppingCart;
    }

    public void AddItem(CartItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(CartItem item)
    {
        _items.Remove(item);
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