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
            IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
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
            SeedProducts(connection, configuration);
        }

        private static void SeedProducts(IDbConnection connection, IConfiguration configuration)
        {
            // Check if products already exist
            var productCount = connection.QuerySingle<int>("SELECT COUNT(*) FROM products");

            if (productCount > 0)
            {
                return; // Products already seeded
            }

            // Configuration for image URLs - Get from environment/configuration
            var baseUrl = configuration["PRODUCT_IMAGE_BASE_URL"] ?? "https://localhost:5001";
            var baseImageUrl = $"{baseUrl}/images/products";

            // Add some specific products for testing
            var specificProducts = new List<ProductSeedData>
                {
                    // Mobile Phones
                    new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "iPhone 15 Pro",
                        Description = "The latest iPhone with advanced Pro camera system, Action Button, and titanium design.",
                        Price = 999.99m,
                        PictureUrl = $"{baseImageUrl}/phone1.jpg",
                        Currency = "USD",
                        Brand = "Apple",
                        Type = "Mobile",
                        QuantityInStock = 50,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-30),
                        UpdatedAt = null
                    },
                      new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Samsung Galaxy S24 Ultra",
                        Description = "Epic in every way. Now with Galaxy AI for next-level creativity and productivity.",
                        Price = 1199.99m,
                        PictureUrl = $"{baseImageUrl}/phone2.jpg",
                        Currency = "USD",
                        Brand = "Samsung",
                        Type = "Mobile",
                        QuantityInStock = 75,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-20),
                        UpdatedAt = null
                    },
                        new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Apple Iphone 17",
                        Description = "Apple Iphone 17 Now with AI for next-level creativity and productivity.",
                        Price = 1199.99m,
                        PictureUrl = $"{baseImageUrl}/phone3.jpg",
                        Currency = "USD",
                        Brand = "Xiaomi",
                        Type = "Mobile",
                        QuantityInStock = 75,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-20),
                        UpdatedAt = null
                    },
                        // Laptops
                    new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "MacBook Pro 16-inch",
                        Description = "Supercharged by M3 Pro and M3 Max chips. Up to 22 hours of battery life.",
                        Price = 2499.99m,
                        PictureUrl = $"{baseImageUrl}/laptop1.jpg",
                        Currency = "USD",
                        Brand = "Apple",
                        Type = "Laptop",
                        QuantityInStock = 25,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-45),
                        UpdatedAt = null
                    },
                      new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Microsoft Surface Pro 9",
                        Description = "2-in-1 laptop and tablet with touchscreen and Surface Pen support.",
                        Price = 1099.99m,
                        PictureUrl = $"{baseImageUrl}/laptop2.jpg",
                        Currency = "USD",
                        Brand = "Microsoft",
                        Type = "Laptop",
                        QuantityInStock = 60,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-40),
                        UpdatedAt = null
                    },
                  new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Dell XPS 13",
                        Description = $"{baseImageUrl}/laptop3.jpg",
                        Price = 1299.99m,
                        PictureUrl = "https://picsum.photos/400/400?random=dell",
                        Currency = "USD",
                        Brand = "Dell",
                        Type = "Laptop",
                        QuantityInStock = 35,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-15),
                        UpdatedAt = null
                    },
                  // Headphones
                    new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sony WH-1000XM5 Headphones",
                        Description = "Industry-leading noise canceling with new Integrated Processor V1.",
                        Price = 399.99m,
                        PictureUrl = $"{baseImageUrl}/headphone1.jpg",
                        Currency = "USD",
                        Brand = "Sony",
                        Type = "Audio",
                        QuantityInStock = 100,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-60),
                        UpdatedAt = null
                    },
                     new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sony WH-1000XM5 Headphones",
                        Description = "Industry-leading noise canceling with new Integrated Processor V1.",
                        Price = 399.99m,
                        PictureUrl = $"{baseImageUrl}/headphone2.jpg",
                        Currency = "USD",
                        Brand = "Beats",
                        Type = "Audio",
                        QuantityInStock = 100,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-60),
                        UpdatedAt = null
                    },
                      new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sony WH-1000XM5 Headphones",
                        Description = "Industry-leading noise canceling with new Integrated Processor V1.",
                        Price = 399.99m,
                        PictureUrl = $"{baseImageUrl}/headphone3.jpg",
                        Currency = "USD",
                        Brand = "Apple",
                        Type = "Audio",
                        QuantityInStock = 100,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-60),
                        UpdatedAt = null
                    },
                      // Keyboards
                    new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Logitect G893",
                        Description = "Logictech G813 best keybroad in the world",
                        Price = 349.99m,
                        PictureUrl = $"{baseImageUrl}/keybroad1.jpg",
                        Currency = "USD",
                        Brand = "Logitech",
                        Type = "Keyboard",
                        QuantityInStock = 0, // Out of stock for testing
                        Status = 1, // Published but out of stock
                        CreatedAt = DateTime.UtcNow.AddDays(-90),
                        UpdatedAt = null
                    },
                     new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Logitect G893",
                        Description = "Logictech G813 best keybroad in the world",
                        Price = 349.99m,
                        PictureUrl = $"{baseImageUrl}/keybroad2.jpg",
                        Currency = "USD",
                        Brand = "Monster",
                        Type = "Keyboard",
                        QuantityInStock = 0, // Out of stock for testing
                        Status = 1, // Published but out of stock
                        CreatedAt = DateTime.UtcNow.AddDays(-90),
                        UpdatedAt = null
                    }, new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Logitect G893",
                        Description = "Logictech G813 best keybroad in the world",
                        Price = 349.99m,
                        PictureUrl = $"{baseImageUrl}/keybroad3.jpg",
                        Currency = "USD",
                        Brand = "Keychron",
                        Type = "Keyboard",
                        QuantityInStock = 0, // Out of stock for testing
                        Status = 1, // Published but out of stock
                        CreatedAt = DateTime.UtcNow.AddDays(-90),
                        UpdatedAt = null
                    },
                     // Speakers
                    new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sony Alpha A7 IV",
                        Description = "Full-frame mirrorless camera with advanced hybrid AF and 4K video recording.",
                        Price = 2499.99m,
                        PictureUrl = $"{baseImageUrl}/speaker1.jpg",
                        Currency = "USD",
                        Brand = "Sony",
                        Type = "Speaker",
                        QuantityInStock = 15,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-25),
                        UpdatedAt = null
                    },
                     new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sony Alpha A7 IV",
                        Description = "Full-frame mirrorless camera with advanced hybrid AF and 4K video recording.",
                        Price = 2499.99m,
                        PictureUrl = $"{baseImageUrl}/speaker2.jpg",
                        Currency = "USD",
                        Brand = "B&0",
                        Type = "Speaker",
                        QuantityInStock = 15,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-25),
                        UpdatedAt = null
                    },
                      new ProductSeedData
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sony Alpha A7 IV",
                        Description = "Full-frame mirrorless camera with advanced hybrid AF and 4K video recording.",
                        Price = 2499.99m,
                        PictureUrl = $"{baseImageUrl}/speaker3.jpg",
                        Currency = "USD",
                        Brand = "Audio Pro",
                        Type = "Speaker",
                        QuantityInStock = 15,
                        Status = 1, // Published
                        CreatedAt = DateTime.UtcNow.AddDays(-25),
                        UpdatedAt = null
                    },

                };

            // Insert products into database - Updated SQL to include Type field
            var insertSql = @"
                    INSERT INTO products (id, name, description, price, picture_url, currency, brand, type, quantity_in_stock, status, created_at, updated_at) 
                    VALUES (@Id, @Name, @Description, @Price, @PictureUrl, @Currency, @Brand, @Type, @QuantityInStock, @Status, @CreatedAt, @UpdatedAt)";

            connection.Execute(insertSql, specificProducts);

            Console.WriteLine($"Successfully seeded {specificProducts.Count} products using Bogus library.");
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
            public string Type { get; set; } = string.Empty; // Added Type field
            public int QuantityInStock { get; set; }
            public int Status { get; set; } // 0=Draft, 1=Published, 2=Unpublished
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }
}
