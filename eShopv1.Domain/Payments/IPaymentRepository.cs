namespace eShopv1.Domain.Payments;

public interface IPaymentRepository
{
    Task<Payment> GetByIdAsync(Guid id);

    void Add(Payment payment);

    void Update(Payment payment);
}