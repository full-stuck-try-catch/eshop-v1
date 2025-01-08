namespace eShopv1.Domain.Orders;

public sealed record ProductItemOrdered(Guid ProductId, string ProductName, string PictureUrl);
