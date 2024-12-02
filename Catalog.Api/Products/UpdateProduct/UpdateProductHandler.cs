// ReSharper disable ClassNeverInstantiated.Global

using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageFile)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(Product Product);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        
        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateProductHandler(IDocumentSession session) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {    
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        
        if (product is null)
            throw new ProductNotFoundException(command.Id);
        
        product.Name = command.Name;
        product.Categories = command.Categories;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(product);
    }
}