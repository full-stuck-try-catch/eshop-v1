using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Carts.DeleteCart;

public sealed record DeleteCartCommand(Guid CartId) : ICommand;