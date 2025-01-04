using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopv1.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<User> GetCurrentUserLogin(string email, CancellationToken cancellationToken = default);

        ValueTask<bool> IsExistedUser(string email, CancellationToken cancellationToken = default);

        void Add(User user);

        void Update(User user);
    }
}
