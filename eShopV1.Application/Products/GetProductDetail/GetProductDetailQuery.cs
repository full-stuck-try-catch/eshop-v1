using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Products.GetProductDetail;

public sealed record GetProductDetailQuery(Guid ProductId) : IQuery<ProductDetailResponse>;