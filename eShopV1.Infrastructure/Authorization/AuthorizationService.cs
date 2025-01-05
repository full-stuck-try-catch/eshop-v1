using eShopv1.Domain.Users;
using eShopV1.Application.Caching;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopV1.Infrastructure.Authorization
{
    internal sealed class AuthorizationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public AuthorizationService(ApplicationDbContext dbContext , ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(Guid userId)
        {
            string cacheKey = $"auth:roles-{userId}";
            UserRolesResponse? cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

            if (cachedRoles is not null)
            {
                return cachedRoles;
            }

            UserRolesResponse roles = await _dbContext.Set<User>()
                .Where(u => u.Id == userId)
                .Select(u => new UserRolesResponse
                {
                    UserId = u.Id,
                    Roles = u.Roles.ToList()
                })
                .FirstAsync();

            await _cacheService.SetAsync(cacheKey, roles);

            return roles;
        }

        public async Task<HashSet<string>> GetPermissionsForUserAsync(Guid userId)
        {
            string cacheKey = $"auth:permissions-{userId}";
            HashSet<string>? cachedPermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

            if (cachedPermissions is not null)
            {
                return cachedPermissions;
            }

            ICollection<Permission> permissions = await _dbContext.Set<User>()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles.Select(r => r.Permissions))
                .FirstAsync();

            var permissionsSet = permissions.Select(p => p.Name).ToHashSet();

            await _cacheService.SetAsync(cacheKey, permissionsSet);

            return permissionsSet;
        }
    }
}
