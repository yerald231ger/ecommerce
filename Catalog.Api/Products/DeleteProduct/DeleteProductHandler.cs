// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;

namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id)
    : ICommand<DeleteProductResult>;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator
    : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        
        if (product is null)
            return new DeleteProductResult(false);
        
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}