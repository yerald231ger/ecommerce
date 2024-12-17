namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(
    IOrderingUnitOfWork unit
)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = (OrderId)command.Order.Id;
        var order = await unit.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
        
        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }
        
        UpdateOrder(order, command.Order);

        unit.Orders.Update(order);
        await unit.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(order.Id);
    }

    private static void UpdateOrder(Order order, OrderDto orderDto)
    {
        var orderName = (OrderName)orderDto.OrderName;
        
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
        
        order.Update(
            orderName,
            shippingAddress,
            billingAddress,
            payment,
            orderDto.Status
        );
    }
}