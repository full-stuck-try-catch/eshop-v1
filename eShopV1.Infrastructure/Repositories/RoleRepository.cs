using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eShopv1.Domain.Users;

namespace eShopV1.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
        }
    }
}