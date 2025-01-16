using eShopv1.Domain.Abstractions;
using eShopV1.Application.Caching;
using MediatR;

namespace eShopV1.Application.CartGuests.DeleteGuestCart;

internal sealed class DeleteGuestCartCommandHandler : IRequestHandler<DeleteGuestCartCommand, Result<string>>
{
    private readonly ICacheService _cacheService;

    public DeleteGuestCartCommandHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Result<string>> Handle(DeleteGuestCartCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"guest_cart:{request.GuestId}";
        
        await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        
        return Result.Success(request.GuestId);
    }
}