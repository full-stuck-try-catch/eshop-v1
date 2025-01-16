using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Products.GetBrands;

public sealed record GetBrandsQuery() : IQuery<List<string>>;