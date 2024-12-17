namespace Ordering.Application.Orders.Queries.GetOrdersByCostumer;

public class GetOrdersByCostumerHandler(
    IOrderingUnitOfWork unit
)
    : IRequestHandler<GetOrdersByCostumerQuery, GetOrdersByCostumerResult>
{
    public async Task<GetOrdersByCostumerResult> Handle(GetOrdersByCostumerQuery request, CancellationToken cancellationToken)
    {
        var orders = await unit.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == (CustomerId)request.CustomerId)
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);
        
        var ordersToDtos = orders.ToDto();
        
        return new GetOrdersByCostumerResult(ordersToDtos);
    }
}