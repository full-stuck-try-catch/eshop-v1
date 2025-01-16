using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Products.GetTypes;

public sealed record GetTypesQuery() : IQuery<List<string>>;