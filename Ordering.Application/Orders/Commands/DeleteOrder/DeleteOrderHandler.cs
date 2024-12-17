namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IOrderingUnitOfWork unit) 
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = (OrderId)command.Id;
        
        var order = await unit.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
        
        if (order is null)
            throw new OrderNotFoundException(orderId);
        
        unit.Orders.Remove(order);
        await unit.SaveChangesAsync(cancellationToken);
        
        return new DeleteOrderResult(true);
    }
}