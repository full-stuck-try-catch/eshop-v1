using eShopv1.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Infrastructure.Authorization
{
    public class UserRolesResponse
    {
        public Guid UserId { get; init; }

        public List<Role> Roles { get; init; } = [];
    }
}
