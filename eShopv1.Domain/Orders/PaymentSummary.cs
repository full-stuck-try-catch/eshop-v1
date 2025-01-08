using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Orders;

public sealed record PaymentSummary(int Last4, string Brand, int ExpMonth, int ExpYear);