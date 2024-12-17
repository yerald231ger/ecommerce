// ReSharper disable ClassNeverInstantiated.Global
namespace Ordering.Application.Orders.Queries.GetOrdersByCostumer;

public record GetOrdersByCostumerQuery(Guid CustomerId) : IQuery<GetOrdersByCostumerResult>;

public record GetOrdersByCostumerResult(IEnumerable<OrderDto> Orders);