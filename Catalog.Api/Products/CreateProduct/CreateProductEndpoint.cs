// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;

namespace Catalog.Api.Products.CreateProduct;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record CreateProductRequest(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageFile);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var product = await sender.Send(command);

                var response = product.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDescription("Create a new product in the catalog");
    }
}