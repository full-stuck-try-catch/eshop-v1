using Bogus;
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

            // Seed Products using Bogus
            SeedProducts(connection);
        }

        private static void SeedProducts(IDbConnection connection)
        {
            // Check if products already exist
            var productCount = connection.QuerySingle<int>("SELECT COUNT(*) FROM products");
            
            if (productCount > 0)
            {
                return; // Products already seeded
            }

            // Define tech brands and categories for realistic data
            var techBrands = new[] { "Apple", "Samsung", "Microsoft", "Google", "Sony", "Dell", "HP", "Lenovo", "ASUS", "Nintendo" };
            var currencies = new[] { "USD", "EUR", "GBP" };
            var categories = new[] { "Smartphone", "Laptop", "Tablet", "Headphones", "Gaming Console", "Smartwatch", "Camera", "Monitor" };

            // Create Bogus faker for products
            var productFaker = new Faker<ProductSeedData>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, (f, p) => 
                {
                    var category = f.PickRandom(categories);
                    var brand = f.PickRandom(techBrands);
                    var model = f.Commerce.ProductName();
                    return $"{brand} {category} {model}";
                })
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(50, 5000))
                .RuleFor(p => p.PictureUrl, f => f.Image.PicsumUrl(400, 400))
                .RuleFor(p => p.Currency, f => f.PickRandom(currencies))
                .RuleFor(p => p.Brand, f => f.PickRandom(techBrands))
                .RuleFor(p => p.QuantityInStock, f => f.Random.Int(0, 1000))
                .RuleFor(p => p.Status, f => f.PickRandom(0, 1, 1, 1, 2)) // Weighted: mostly Published (1)
                .RuleFor(p => p.CreatedAt, f => f.Date.Between(DateTime.UtcNow.AddYears(-2), DateTime.UtcNow))
                .RuleFor(p => p.UpdatedAt, (f, p) => null); // 30% chance of being null

            // Generate 100 fake products
            var products = productFaker.Generate(100);

            // Add some specific products for testing
            var specificProducts = new List<ProductSeedData>
            {
                new ProductSeedData
                {
                    Id = Guid.NewGuid(),
                    Name = "iPhone 15 Pro",
                    Description = "The latest iPhone with advanced Pro camera system, Action Button, and titanium design.",
                    Price = 999.99m,
                    PictureUrl = "https://picsum.photos/400/400?random=iphone",
                    Currency = "USD",
                    Brand = "Apple",
                    QuantityInStock = 50,
                    Status = 1, // Published
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    UpdatedAt = null
                },
                new ProductSeedData
                {
                    Id = Guid.NewGuid(),
                    Name = "MacBook Pro 16-inch",
                    Description = "Supercharged by M3 Pro and M3 Max chips. Up to 22 hours of battery life.",
                    Price = 2499.99m,
                    PictureUrl = "https://picsum.photos/400/400?random=macbook",
                    Currency = "USD",
                    Brand = "Apple",
                    QuantityInStock = 25,
                    Status = 1, // Published
                    CreatedAt = DateTime.UtcNow.AddDays(-45),
                    UpdatedAt = null
                },
                new ProductSeedData
                {
                    Id = Guid.NewGuid(),
                    Name = "Samsung Galaxy S24 Ultra",
                    Description = "Epic in every way. Now with Galaxy AI for next-level creativity and productivity.",
                    Price = 1199.99m,
                    PictureUrl = "https://picsum.photos/400/400?random=samsung",
                    Currency = "USD",
                    Brand = "Samsung",
                    QuantityInStock = 75,
                    Status = 1, // Published
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    UpdatedAt = null
                },
                new ProductSeedData
                {
                    Id = Guid.NewGuid(),
                    Name = "Sony WH-1000XM5 Headphones",
                    Description = "Industry-leading noise canceling with new Integrated Processor V1.",
                    Price = 399.99m,
                    PictureUrl = "https://picsum.photos/400/400?random=headphones",
                    Currency = "USD",
                    Brand = "Sony",
                    QuantityInStock = 100,
                    Status = 1, // Published
                    CreatedAt = DateTime.UtcNow.AddDays(-60),
                    UpdatedAt = null
                },
                new ProductSeedData
                {
                    Id = Guid.NewGuid(),
                    Name = "Nintendo Switch OLED",
                    Description = "Vibrant 7-inch OLED screen, enhanced audio, and improved kickstand.",
                    Price = 349.99m,
                    PictureUrl = "https://picsum.photos/400/400?random=switch",
                    Currency = "USD",
                    Brand = "Nintendo",
                    QuantityInStock = 0, // Out of stock for testing
                    Status = 1, // Published but out of stock
                    CreatedAt = DateTime.UtcNow.AddDays(-90),
                    UpdatedAt = null
                }
            };

            // Combine generated and specific products
            products.AddRange(specificProducts);

            // Insert products into database
            var insertSql = @"
                INSERT INTO products (id, name, description, price, picture_url, currency, brand, quantity_in_stock, status, created_at, updated_at) 
                VALUES (@Id, @Name, @Description, @Price, @PictureUrl, @Currency, @Brand, @QuantityInStock, @Status, @CreatedAt, @UpdatedAt)";

            connection.Execute(insertSql, products);

            Console.WriteLine($"Successfully seeded {products.Count} products using Bogus library.");
        }

        private class ProductSeedData
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string PictureUrl { get; set; } = string.Empty;
            public string Currency { get; set; } = string.Empty;
            public string Brand { get; set; } = string.Empty;
            public int QuantityInStock { get; set; }
            public int Status { get; set; } // 0=Draft, 1=Published, 2=Unpublished
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }
}
