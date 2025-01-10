using eShopv1.Domain.Orders;
using eShopv1.Domain.Payments;
using eShopv1.Domain.Products;
using eShopv1.Domain.ShoppingCarts;
using eShopv1.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace eShopV1.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get;}

        DbSet<Role> Roles { get; }

        DbSet<Permission> Permissions { get; }

        DbSet<Product> Products { get; }

        DbSet<ShoppingCart> ShoppingCarts { get; }

        DbSet<CartItem> CartItems { get; }

        DbSet<Order> Orders { get; }

        DbSet<OrderItem> OrderItems { get; }

        DbSet<DeliveryMethod> DeliveryMethods { get; }

        DbSet<Payment> Payments { get; }
    }
}
