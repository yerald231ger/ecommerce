namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
    
    public static implicit operator Guid(ProductId productId) => productId.Value;
    public static explicit operator ProductId(Guid productId) => ProductId.Of(productId);
    
    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new DomainException($"The {nameof(ProductId)} cannot be empty.");
        
        return new ProductId(productId);
    }
}