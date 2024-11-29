// ReSharper disable ClassNeverInstantiated.Global

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageFile)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = command.Id,
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}