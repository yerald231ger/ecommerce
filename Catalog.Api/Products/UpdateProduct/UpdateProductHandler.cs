// ReSharper disable ClassNeverInstantiated.Global

using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageFile)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle product {@Id}", command.Id);
        
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        
        if (product is null)
            return new UpdateProductResult(false);
        
        product.Name = command.Name;
        product.Categories = command.Categories;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }
}