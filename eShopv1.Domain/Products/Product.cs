using eShopv1.Domain.Abstractions;
using eShopv1.Domain.Products.Events;

namespace eShopv1.Domain.Products
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string PictureUrl { get; private set; }
        public string Currency { get; private set; }
        public string Brand { get; private set; }
        public int QuantityInStock { get; private set; }
        public ProductStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Product() { }

        private Product(Guid id, string name, string description, decimal price, string pictureUrl, string brand, int quantityInStock, ProductStatus status, DateTime createdAt, DateTime updatedAt) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = pictureUrl;    
            Brand = brand;
            QuantityInStock = quantityInStock;
            Status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Product Create(Guid id, string name, string description, decimal price, string pictureUrl, string type, string brand, int quantityInStock, ProductStatus status, DateTime createdAt, DateTime updatedAt)
        {
            var product = new Product(id, name, description, price, pictureUrl, brand, quantityInStock, status, createdAt, updatedAt);

            product.RaiseDomainEvent(new ProductCreatedEvent(product.Id, product.Name, product.Brand, product.QuantityInStock));

            return product;
        }

        public void Update(string name, string description, decimal price, string pictureUrl, string brand, int quantityInStock, ProductStatus status, DateTime updatedAt)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = pictureUrl;
            Brand = brand;
            QuantityInStock = quantityInStock;
            Status = status;
            UpdatedAt = updatedAt;
        }
    }
}