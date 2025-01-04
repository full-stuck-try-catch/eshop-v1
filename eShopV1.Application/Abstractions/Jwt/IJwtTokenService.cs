using eShopv1.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Application.Abstractions.Jwt
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
