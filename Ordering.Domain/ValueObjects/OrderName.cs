namespace Ordering.Domain.ValueObjects;

public record class OrderName
{
    private const int _defaultLength = 100;
    public string Value { get; }
    
    public static implicit operator string(OrderName orderName) => orderName.Value;
    public static explicit operator OrderName(string orderName) => OrderName.Of(orderName);
    
    private OrderName(string value) => Value = value;
    
    public static OrderName Of(string orderName)
    {
        if (string.IsNullOrWhiteSpace(orderName))
            throw new DomainException($"The {nameof(OrderName)} cannot be empty.");
        
        if (orderName.Length > _defaultLength)
            throw new DomainException($"The {nameof(OrderName)} cannot be greater than {_defaultLength}.");
        
        return new OrderName(orderName);
    }
}