using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts;

public sealed record ShoppingCartErrors
{
    public static readonly Error ShoppingCartNotFound = new("ShoppingCart.NotFound", "Shopping cart not found.");
    public static readonly Error ShoppingCartAlreadyExists = new("ShoppingCart.AlreadyExists", "Shopping cart already exists.");
    public static readonly Error ShoppingCartItemNotFound = new("ShoppingCartItem.NotFound", "Shopping cart item not found.");
    public static readonly Error ShoppingCartItemAlreadyExists = new("ShoppingCartItem.AlreadyExists", "Shopping cart item already exists.");
    public static readonly Error CouponNotFound = new("Coupon.NotFound", "Coupon not found.");
    public static readonly Error InvalidQuantity = new("InvalidQuantity", "Invalid quantity.");
    public static readonly Error ItemAlreadyExists = new("Item.AlreadyExists", "Item already exists.");
    public static readonly Error ItemNotFound = new("Item.NotFound", "Item not found.");
}