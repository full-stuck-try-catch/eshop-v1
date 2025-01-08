using eShopv1.Domain.Abstractions;
using eShopv1.Domain.ShoppingCarts;

namespace eShopv1.Domain.Orders;

public sealed class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = new List<OrderItem>();

    public IReadOnlyList<OrderItem> Items => _items.ToList();
    public Guid UserId { get; private set; }
    // Pre-discount/shipping/tax
    public decimal Subtotal { get; set; }
    // After discount/shipping/tax
    public decimal Discount { get; set; }
    public string Currency { get; private set; }
    public Guid? CouponId { get; private set; }
    public DeliveryMethod DeliveryMethod { get; private set; }
    public ShippingAddress ShippingAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime OrderDate { get; private set; }

    private Order(Guid id) : base(id) { }

    private Order(Guid id, Guid userId, decimal subtotal, decimal discount, DeliveryMethod deliveryMethod, ShippingAddress shippingAddress, OrderStatus status, DateTime orderDate) : base(id)
    {
        UserId = userId;
        Subtotal = subtotal;
        Discount = discount;
        DeliveryMethod = deliveryMethod;
        Status = status;
        OrderDate = orderDate;
        ShippingAddress = shippingAddress;
    }

    public static Result<Order> Create(Guid id, Guid userId, DeliveryMethod deliveryMethod, ShippingAddress shippingAddress, DateTime orderDate, AppCoupon? coupon, List<OrderItem> items, OrderService orderService)
    {
        if(!items.Any()){
            return Result.Failure<Order>(OrderErrors.OrderItemsRequired);
        }

        if(shippingAddress is null){
            return Result.Failure<Order>(OrderErrors.ShippingAddressRequired);
        }

        if(deliveryMethod is null){
            return Result.Failure<Order>(OrderErrors.DeliveryMethodRequired);
        }

        var priceDetail = orderService.CalculateOrderPrice(items, coupon);

        var order = new Order(id, userId, priceDetail.Subtotal, priceDetail.Discount, deliveryMethod, shippingAddress, OrderStatus.Pending, orderDate);
        order._items.AddRange(items);
        return Result.Success(order);
    }

    public Result UpdateOrder(Guid id, Guid userId, DeliveryMethod deliveryMethod, ShippingAddress shippingAddress, DateTime orderDate, AppCoupon? coupon, List<OrderItem> items, OrderService orderService)
    {
        var priceDetail = orderService.CalculateOrderPrice(items, coupon);

        var order = new Order(id, userId, priceDetail.Subtotal, priceDetail.Discount, deliveryMethod, shippingAddress, OrderStatus.Pending, orderDate);
        order._items.AddRange(items);
        return Result.Success(order);
    }   
}