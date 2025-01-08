using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts;

public sealed class CartItem : Entity
{
    public Guid ShoppingCartId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string PictureUrl { get; private set; }
    public string Currency { get; private set; }

    private CartItem() { }

    private CartItem(Guid id, Guid shoppingCartId, Guid productId, string productName, decimal price, int quantity, string pictureUrl, string currency) : base(id)
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
        PictureUrl = pictureUrl;
        Currency = currency;
    }

    public static CartItem Create(Guid id, Guid shoppingCartId, Guid productId, string productName, decimal price, int quantity, string pictureUrl, string currency)
    {
        var cartItem = new CartItem(id, shoppingCartId, productId, productName, price, quantity, pictureUrl, currency);

        return cartItem;
    }

    public void UpdateQuantity(int quantity){
        Quantity = quantity;
    }
}
