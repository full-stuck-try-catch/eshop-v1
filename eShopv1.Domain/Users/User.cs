using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Users.Events;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Threading.Tasks;

namespace eShopv1.Domain.Users
{
    public sealed class User : Entity
    {
        private readonly List<Role> _roles = new();
        public string? UserName { get; private set; }
        public string Email { get; private set; }

        public string? FirstName { get; private set; }

        public string? LastName { get; private set; }

        public string PasswordHash { get; private set; }

        public IReadOnlyCollection<Role> Roles => _roles.ToList();

        public Address? Address { get; private set; }

        private User() { }

        private User(Guid id, string? userName, string email, string? firstName , string? lastName, string passwordHash) : base(id)
        {
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
        }

        public static async Task<Result<User>> Create(Guid userId, string? userName, string email, string? firstName , string? lastName, string passwordHash, IUserRepository userRepository, CancellationToken cancellationToken = default)
        {
            if (await userRepository.IsExistedUser(email, cancellationToken))
                return Result.Failure<User>(UserErrors.EmailAlreadyExists);

            var user = new User(userId, userName, email, firstName, lastName, passwordHash);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(userId, email , userName));

            return Result.Success(user);
        }

        public async Task<Result> UpdateEmail(string email, IUserRepository userRepository)
        {
            if (await userRepository.IsExistedUser(email))
                return Result.Failure(UserErrors.EmailAlreadyExists);

            Email = email;

            return Result.Success();
        }

        public Result ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return Result.Failure(UserErrors.PasswordNullOrEmpty);

            PasswordHash = newPassword;

            RaiseDomainEvent(new UserPasswordChangedDomainEvent(Id, Email, PasswordHash));

            return Result.Success();
        }

        public void AddRole(Role role)
        {
            _roles.Add(role);
        }

        public void UpdateAddress(Address address)
        {
            if (address is null)
                throw new ArgumentNullException(nameof(address), "Address cannot be null");
            Address = address;
        }
    }
}
