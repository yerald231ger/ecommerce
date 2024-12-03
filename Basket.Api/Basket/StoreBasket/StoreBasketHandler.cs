// ReSharper disable ClassNeverInstantiated.Global
namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(string UserName, ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.");

        RuleFor(x => x.Cart)
            .NotNull()
            .WithMessage("Cart is required.");
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;
        
        return Task.FromResult(new StoreBasketResult("Gera"));
    }
}