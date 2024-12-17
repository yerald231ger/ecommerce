// ReSharper disable ClassNeverInstantiated.Global
namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto OrderDto) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDto).NotEmpty().WithMessage("Order is required");
        RuleFor(x => x.OrderDto.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.OrderDto.OrderName).NotEmpty().WithMessage("OrderName is required");
    }
}