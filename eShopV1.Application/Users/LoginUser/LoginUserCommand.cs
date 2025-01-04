using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Users.LoginUser
{
    public sealed record LoginUserCommand(string Email , string Password) : ICommand<LoginUserResponse>;

}
