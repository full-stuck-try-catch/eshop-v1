using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders;

public sealed class DeliveryMethod(string ShortName, string DeliveryTime, string Description, decimal Price);