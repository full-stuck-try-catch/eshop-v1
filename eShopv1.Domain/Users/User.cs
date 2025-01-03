using Microsoft.AspNetCore.Identity;
using System;

namespace eShopv1.Domain.Users
{
    public sealed class User : IdentityUser<Guid>
    {
        public Address? Address { get; set; }
    }
}
