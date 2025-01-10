namespace eShopv1.Domain.Orders;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);

    void Add(Order order);

    void Update(Order order);
   
}