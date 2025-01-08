using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Abstractions;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id){
        
    }
}