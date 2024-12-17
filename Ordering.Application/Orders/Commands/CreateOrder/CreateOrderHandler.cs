namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(
    IOrderingUnitOfWork unit
)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.OrderDto);

        unit.Orders.Add(order);
        await unit.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id);
    }

    private static Order CreateNewOrder(OrderDto orderDto)
    {
        var billingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode);

        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode);

        var payment = Payment.Of(
            orderDto.Payment.CardHolderName,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod);

        var order = Order.Create(
            (OrderId)orderDto.Id,
            (CustomerId)orderDto.CustomerId,
            (OrderName)orderDto.OrderName,
            billingAddress,
            shippingAddress,
            payment
        );
        
        foreach (var item in orderDto.OrderItems)
        {
            order.AddOrderItem(
                (ProductId)item.ProductId,
                item.Quantity,
                item.Price
            );
        }

        return order;
    }
}