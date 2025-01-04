using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Users;
using eShopV1.Application.Abstractions.Data;
using eShopV1.Application.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        // Add DbSet properties for other entities as needed
        // public DbSet<YourEntity> YourEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}