using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts;

public interface IShoppingCartRepository
{
    Task<ShoppingCart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ShoppingCart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    ValueTask<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    void Add(ShoppingCart shoppingCart);
    void Update(ShoppingCart shoppingCart);
    void Delete(ShoppingCart shoppingCart);
}