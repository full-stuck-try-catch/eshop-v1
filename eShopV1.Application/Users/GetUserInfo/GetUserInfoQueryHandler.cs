using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Users;
using eShopV1.Application.Abstractions.Authentication;
using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Users.GetUserInfo
{
    internal sealed class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, GetUserInfoResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public GetUserInfoQueryHandler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<GetUserInfoResponse>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
                return Result.Failure<GetUserInfoResponse>(UserErrors.NotFound);

            var response = new GetUserInfoResponse(
                user.Id,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Address
            );

            return Result.Success(response);
        }
    }
} 