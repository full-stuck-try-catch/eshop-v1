using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Users;
using eShopV1.Application.Abstractions.Authentication;
using eShopV1.Application.Abstractions.Jwt;
using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Users.LoginUser
{
    internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IPasswordHasher _hasher;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IPasswordHasher hasher, IJwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _hasher = hasher;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }
        public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetCurrentUserLogin(request.Email, cancellationToken);

            if(user == null)
                return Result.Failure<LoginUserResponse>(UserErrors.NotFound);

            if (!_hasher.Verify(user.PasswordHash, request.Password))
                return Result.Failure<LoginUserResponse>(UserErrors.InvalidUserPassword);

            var token = _jwtTokenService.GenerateToken(user);

            return new LoginUserResponse(user.Id, user.Email, token);
        }
    }
}
