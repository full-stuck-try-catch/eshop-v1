using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.ShoppingCarts;

public interface IShoppingCartRepository
{
    Task<ShoppingCart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ShoppingCart?> GetCartByBuyerIdAsync(Guid userId, CancellationToken cancellationToken = default);

    ValueTask<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<bool> IsExistByBuyerIdAsync(Guid userId, CancellationToken cancellationToken = default);

    void Add(ShoppingCart shoppingCart);
    void Update(ShoppingCart shoppingCart);

    void Remove(ShoppingCart shoppingCart);
}