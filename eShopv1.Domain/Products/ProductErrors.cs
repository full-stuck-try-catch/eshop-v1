using eShopv1.Domain.Abstractions;

namespace eShopv1.Domain.Products;

public static class ProductErrors
{
    public static Error NotFound => new(
        "Product.NotFound",
        "The product with the specified identifier was not found");

    public static Error NotAvailable => new(
        "Product.NotAvailable", 
        "The product is not available");
}