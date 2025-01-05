using Dapper;
using eShopV1.Application.Abstractions.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace eShopV1.API.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using IDbConnection connection = sqlConnectionFactory.CreateConnection();

            var adminRoleId = connection.QuerySingle<int>("SELECT id FROM roles WHERE name = @Name", new { Name = "Admin" });

            // Seed admin user
            var id = Guid.NewGuid();
            var adminUserName = "admin";
            var adminEmail = "admin@yourdomain.com";
            var firstName = "admin";
            var lastName = "admin";
            var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123"); // Use your password hashing here!

            var adminUserId = connection.QueryFirstOrDefault<Guid?>(
                "SELECT id FROM users WHERE user_name = @UserName", new { UserName = adminUserName });

            if (adminUserId == null)
            {
                connection.Execute(
                    "INSERT INTO users (id ,user_name, email, first_name, last_name, password_hash) VALUES (@Id, @UserName, @Email, @FirstName , @LastName, @PasswordHash)",
                    new { Id = id, UserName = adminUserName, Email = adminEmail, FirstName = firstName, LastName = lastName, PasswordHash = adminPassword });

                adminUserId = connection.QuerySingle<Guid>("SELECT id FROM users WHERE user_name = @UserName", new { UserName = adminUserName });
            }

            // Assign admin role to admin user
            var existsUserRole = connection.QueryFirstOrDefault<int>(
                "SELECT 1 FROM user_roles WHERE user_id = @UserId AND role_id = @RoleId",
                new { UserId = adminUserId, RoleId = adminRoleId });

            if (existsUserRole == 0)
            {
                connection.Execute(
                    "INSERT INTO user_roles (user_id, role_id) VALUES (@UserId, @RoleId)",
                    new { UserId = adminUserId, RoleId = adminRoleId });
            }
        }
    }
}
