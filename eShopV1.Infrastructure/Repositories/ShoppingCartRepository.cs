using eShopv1.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Infrastructure.Repositories;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ShoppingCart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<ShoppingCart?> GetCartByBuyerIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.BuyerId == userId, cancellationToken);
    }

    public async ValueTask<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.ShoppingCarts
            .AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async ValueTask<bool> IsExistByBuyerIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.ShoppingCarts
            .AnyAsync(c => c.BuyerId == userId, cancellationToken);
    }
}