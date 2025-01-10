using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The provided credentials were invalid");

        public static readonly Error PasswordNullOrEmpty = new(
            "User.PasswordNullOrEmpty",
            "The password cannot be null or empty");

        public static readonly Error EmailAlreadyExists = new(
            "User.EmailAlreadyExists",
            "The email address is already associated with another account");

        public static readonly Error InvalidUserPassword = new(
            "User.InvalidUserPassword",
            "The provided user password is invalid");

        public static readonly Error DefaultRoleNotFound = new(
            "User.DefultRoleNotFound",
            "The default role for the user was not found");
    }
}
