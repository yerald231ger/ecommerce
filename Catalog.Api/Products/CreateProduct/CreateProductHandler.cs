// ReSharper disable ClassNeverInstantiated.Global

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    decimal Price,
    string ImageFile)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required and must be a valid GUID");
        RuleFor(x => x.Name).NotEmpty().WithName("Product Name is required and cannot be empty");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("At least one category is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

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