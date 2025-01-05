using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Application.Abstractions.Authentication
{
    public interface IUserContext
    {
        Guid UserId { get; }

        string Email { get; }
    }
}
