using System.Threading;
using System.Threading.Tasks;

namespace eShopv1.Domain.Users
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}