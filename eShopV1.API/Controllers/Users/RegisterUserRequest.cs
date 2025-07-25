namespace eShopV1.API.Controllers.Users
{
    public class RegisterUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? UserName { get; set; }
    }
}