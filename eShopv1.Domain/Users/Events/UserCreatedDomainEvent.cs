using eShopv1.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopv1.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId, string Email , string? UserName ) : IDomainEvent;
}
