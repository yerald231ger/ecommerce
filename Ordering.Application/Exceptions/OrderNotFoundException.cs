using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException(OrderId orderId) : NotFoundException($"Order with id {orderId} was not found");