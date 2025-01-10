using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders;

public sealed class DeliveryMethod : Entity
{
    private DeliveryMethod() { }

    private DeliveryMethod(Guid id, string shortName, string deliveryTime, string description, decimal price) : base(id)
    {
        ShortName = shortName;
        DeliveryTime = deliveryTime;
        Description = description;
        Price = price;
    }

    public static DeliveryMethod Create(Guid id, string shortName, string deliveryTime, string description, decimal price)
    {
        return new DeliveryMethod(id, shortName, deliveryTime, description, price);
    }

    public string ShortName { get; private set; }
    public string DeliveryTime { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
}
