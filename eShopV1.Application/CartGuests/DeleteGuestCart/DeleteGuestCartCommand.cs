using eShopv1.Domain.Abstractions;
using MediatR;

namespace eShopV1.Application.CartGuests.DeleteGuestCart;

public sealed record DeleteGuestCartCommand(string GuestId) : IRequest<Result<string>>;