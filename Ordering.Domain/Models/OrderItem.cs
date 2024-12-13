namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public required OrderId OrderId { get; init; }
    
    public required ProductId ProductId { get; init; }
    
    public int Quantity { get; init; }
    
    public decimal Price { get; init; }
    
    public static OrderItem Create(OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        return new OrderItem
        {
            Id = OrderItemId.Of(Guid.CreateVersion7()),
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            Price = price
        };
    }
}