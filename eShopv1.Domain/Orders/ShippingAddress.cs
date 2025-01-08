namespace eShopv1.Domain.Orders;

using eShopv1.Domain.Abstractions;
public sealed record ShippingAddress(string Name, string Line1, string? Line2, string City, string State, string PostalCode, string Country);