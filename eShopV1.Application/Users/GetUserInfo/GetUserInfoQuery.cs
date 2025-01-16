using eShopV1.Application.Abstractions.Messaging;
 
namespace eShopV1.Application.Users.GetUserInfo
{
    public sealed record GetUserInfoQuery() : IQuery<GetUserInfoResponse>;
} 