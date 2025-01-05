using FluentValidation;

namespace eShopV1.Application.Users.RegisterUser
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email address is required");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long");

            RuleFor(x => x.UserName)
                .MaximumLength(200)
                .WithMessage("Username cannot exceed 100 characters");

            RuleFor(x => x.FirstName)
                .MaximumLength(200)
                .WithMessage("First name cannot exceed 200 characters");

            RuleFor(x => x.LastName)
                .MaximumLength(200)
                .WithMessage("Last name cannot exceed 200 characters");
        }
    }
}