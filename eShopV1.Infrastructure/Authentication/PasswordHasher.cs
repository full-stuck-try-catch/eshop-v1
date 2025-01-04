using eShopV1.Application.Abstractions.Authentication;

namespace eShopV1.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string hash, string password) =>
            BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
