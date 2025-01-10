using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Orders.Events;
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
    public AppCoupon? AppliedCoupon { get; private set; }
    public DeliveryMethod DeliveryMethod { get; private set; } = null!;
    public ShippingAddress ShippingAddress { get; private set; } = null!;
    public OrderStatus Status { get; private set; }
    public DateTime OrderDate { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Order(Guid id, Guid userId, decimal subtotal, decimal discount, string currency, DateTime orderDate) : base(id)
    {
        UserId = userId;
        Subtotal = subtotal;
        Discount = discount;
        Currency = currency;
        Status = OrderStatus.Pending;
        OrderDate = orderDate;
    }

    public static Result<Order> Create(Guid id, Guid userId, string currency, ShippingAddress shippingAddress, DateTime orderDate,DeliveryMethod deliveryMethod, AppCoupon? appliedCoupon, 
        List<OrderItem> items, OrderService orderService)
    {
        if(!items.Any()){
            return Result.Failure<Order>(OrderErrors.OrderItemsRequired);
        }

        if(shippingAddress is null){
            return Result.Failure<Order>(OrderErrors.ShippingAddressRequired);
        }

        var priceDetail = orderService.CalculateOrderPrice(items, appliedCoupon);

        var order = new Order(id, userId, priceDetail.Subtotal, priceDetail.Discount, currency, orderDate);
        order.ShippingAddress = shippingAddress;
        order.AppliedCoupon = appliedCoupon;
        order.DeliveryMethod = deliveryMethod;

        order._items.AddRange(items);

        order.RaiseDomainEvent(new OrderCreatedDomainEvent(id, userId));

        return Result.Success(order);
    }

    public Result UpdateOrder(Guid id, ShippingAddress shippingAddress, DateTime updatedAt, AppCoupon? appliedCoupon, List<OrderItem> items, OrderService orderService)
    {
        var priceDetail = orderService.CalculateOrderPrice(items, appliedCoupon);

        Subtotal = priceDetail.Subtotal;
        Discount = priceDetail.Discount;
        ShippingAddress = shippingAddress;
        UpdatedAt = updatedAt;
        AppliedCoupon = appliedCoupon;
        _items.Clear();
        _items.AddRange(items);
        RaiseDomainEvent(new OrderUpdatedDomainEvent(id, _items, ShippingAddress, AppliedCoupon, UpdatedAt));
        
        return Result.Success();
    }   
}