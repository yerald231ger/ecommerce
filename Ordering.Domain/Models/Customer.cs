namespace Ordering.Domain.Models;
 
public class Customer : Entity<CustomerId>
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public static Customer Create(CustomerId customerId, string name, string email)
    { 
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
        ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));

        var costumer = new Customer
        {
            Id = customerId,
            Name = name,
            Email = email
        };
        
        return costumer;
    }
}