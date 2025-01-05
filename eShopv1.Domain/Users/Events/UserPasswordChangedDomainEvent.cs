using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Users.Events
{
    public sealed record UserPasswordChangedDomainEvent(Guid UserId, string Email, string PasswordHash) : IDomainEvent;
}
