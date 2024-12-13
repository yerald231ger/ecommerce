namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }
    
    public static implicit operator Guid(CustomerId customerId) => customerId.Value;
    public static explicit operator CustomerId(Guid costumerId) => CustomerId.Of(costumerId);
    
    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid costumerId)
    {
        if (costumerId == Guid.Empty)
            throw new DomainException($"The {nameof(CustomerId)} cannot be empty.");
        
        return new CustomerId(costumerId);
    }
}