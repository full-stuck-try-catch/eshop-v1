using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Products;

public interface IProductRepository{
    Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Product product);
    void Update(Product product);
}