namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }
    
    public static implicit operator Guid(OrderItemId orderItemId) => orderItemId.Value;
    public static explicit operator OrderItemId(Guid orderItemId) => OrderItemId.Of(orderItemId);
    
    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid orderItemId)
    {
        if (orderItemId == Guid.Empty)
            throw new DomainException($"The {nameof(CustomerId)} cannot be empty.");
        
        return new OrderItemId(orderItemId);
    }
}