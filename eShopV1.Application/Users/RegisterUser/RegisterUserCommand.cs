using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string? UserName = null) : ICommand<RegisterUserResponse>;
}