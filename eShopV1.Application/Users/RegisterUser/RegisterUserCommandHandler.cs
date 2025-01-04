using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Users;
using eShopV1.Application.Abstractions.Authentication;
using eShopV1.Application.Abstractions.Messaging;

namespace eShopV1.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;

        public RegisterUserCommandHandler(
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IRoleRepository roleRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Hash(request.Password);
            var userId = Guid.NewGuid();

            var registeredRole = await _roleRepository.GetByNameAsync(Role.Registered.Name, cancellationToken);

            if (registeredRole == null)
                return Result.Failure<RegisterUserResponse>(UserErrors.DefaultRoleNotFound);

            var createResult = await User.Create(
                userId,
                request.UserName,
                request.Email,
                hashedPassword,
                _userRepository,
                cancellationToken);

            if (createResult.IsFailure)
                return Result.Failure<RegisterUserResponse>(createResult.Error);

            var createdUser = createResult.Value;

            createdUser.AddRole(registeredRole);

            _userRepository.Add(createdUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterUserResponse(createdUser.Id, createdUser.Email, createdUser.UserName);
        }
    }
}