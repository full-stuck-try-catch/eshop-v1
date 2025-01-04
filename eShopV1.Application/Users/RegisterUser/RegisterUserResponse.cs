namespace eShopV1.Application.Users.RegisterUser
{
    public sealed record RegisterUserResponse(Guid UserId, string Email, string? UserName);
}