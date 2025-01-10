using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Users;
using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using eShopv1.Domain.Products;
using eShopv1.Domain.ShoppingCarts;
using eShopv1.Domain.Orders;
using eShopv1.Domain.Payments;

namespace eShopV1.Infrastructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        private readonly IDateTimeProvider _dateTimeProvider;

        public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public DbSet<User> Users { get; private set; }

        public DbSet<Role> Roles { get; private set; }

        public DbSet<Permission> Permissions { get; private set; }

        public DbSet<Product> Products { get; private set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; private set; }

        public DbSet<CartItem> CartItems { get; private set; }

        public DbSet<Order> Orders { get; private set; }

        public DbSet<OrderItem> OrderItems { get; private set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; private set; }

        public DbSet<Payment> Payments { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}