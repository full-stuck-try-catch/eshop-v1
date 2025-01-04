using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Application.Users.LoginUser
{
    public sealed record LoginUserResponse(Guid UserId, string Email, string Token);
}
