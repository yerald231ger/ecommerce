namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public required string Name { get; init; }
    
    public required decimal Price { get; init; }

    public static Product Create(ProductId productId, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or empty", nameof(name));
        
        if (price <= 0)
            throw new ArgumentException("Product price cannot be less than or equal to zero", nameof(price));
        
        var product = new Product
        {
            Id = productId,
            Name = name,
            Price = price
        };
        
        return product;
    }
}