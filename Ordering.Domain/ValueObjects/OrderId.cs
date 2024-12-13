namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }
    
    public static implicit operator Guid(OrderId orderId) => orderId.Value;
    public static explicit operator OrderId(Guid orderId) => OrderId.Of(orderId);
    
    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid orderId)
    {
        if (orderId == Guid.Empty)
            throw new DomainException($"The {nameof(CustomerId)} cannot be empty.");
        
        return new OrderId(orderId);
    }
}