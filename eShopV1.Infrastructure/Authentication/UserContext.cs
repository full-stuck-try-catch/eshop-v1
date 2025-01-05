using eShopV1.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace eShopV1.Infrastructure.Authentication
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => 
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

        public string Email => _httpContextAccessor
            .HttpContext?
            .User
            .GetEmail() ?? throw new ApplicationException("Email context is unavaiable");
    }
}
