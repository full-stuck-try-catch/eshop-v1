using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders;

public sealed class OrderItem : Entity{
    public ProductItemOrdered ProductItemOrdered { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem() { }

    private OrderItem(Guid id, ProductItemOrdered productItemOrdered, decimal price, int quantity) : base(id)
    {
        ProductItemOrdered = productItemOrdered;
        Price = price;
        Quantity = quantity;
    }

    public static OrderItem Create(Guid id, ProductItemOrdered productItemOrdered, decimal price, int quantity)
    {
        var orderItem = new OrderItem(id, productItemOrdered, price, quantity);
        return orderItem;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}