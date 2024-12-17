namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(
    IOrderingUnitOfWork unit
) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var pageIndex = request.PaginatedRequest.PageIndex;
        var pageSize = request.PaginatedRequest.PageSize;
        var totalOrders = await unit.Orders.LongCountAsync(cancellationToken);

        var orders = await unit.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var ordersDto = orders.ToDto();
        
        var paginatedResult = new PaginatedResult<OrderDto>(
            pageIndex, 
            pageSize, 
            totalOrders,
            ordersDto);
        
        return new GetOrdersResult(paginatedResult);
    }
}