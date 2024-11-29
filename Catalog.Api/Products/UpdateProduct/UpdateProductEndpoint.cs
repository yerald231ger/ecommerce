// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;

namespace Catalog.Api.Products.UpdateProduct;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateProductRequest(
    string Name,
    List<string> Categories,
    string Description,
    decimal Price,
    string ImageFile);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                command = command with { Id = id };

                var result = await sender.Send(command);
                
                var response = result.Adapt<UpdateProductResponse>();

                return !response.IsSuccess ? Results.NotFound() : Results.NoContent();
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update a product")
            .WithDescription("Update a product in the catalog");
    }
}