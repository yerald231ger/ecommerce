namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(
    IOrderingUnitOfWork unit
)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await unit.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.OrderName))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        var ordersToDtos = orders.ToDto();
        return new GetOrdersByNameResult(ordersToDtos);
    }
}