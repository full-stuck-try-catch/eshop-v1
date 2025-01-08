using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders;

public sealed record OrderPriceDetail(decimal Subtotal, decimal Discount);