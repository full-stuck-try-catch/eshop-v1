using eShopv1.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Infrastructure.Repositories
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<User?> GetCurrentUserLogin(string email, CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<User>()
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken); 
        }

        public async ValueTask<bool> IsExistedUser(string email, CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<User>()
                .AsNoTracking()
                .AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
