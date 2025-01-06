using eShopv1.Domain.Users;

namespace eShopV1.Application.Users.GetUserInfo
{
    public sealed record GetUserInfoResponse(
        Guid Id,
        string? UserName,
        string Email,
        string? FirstName,
        string? LastName,
        Address? Address
    );
} 