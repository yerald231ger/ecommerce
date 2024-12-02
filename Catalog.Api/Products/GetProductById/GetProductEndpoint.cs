// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;

namespace Catalog.Api.Products.GetProductById;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetProductByIdQuery(id);
        
                var result = await sender.Send(query);
        
                var response = result.Adapt<GetProductByIdResponse>();
        
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get a product by ID")
            .WithDescription("Get a product by its unique identifier");
    }
}