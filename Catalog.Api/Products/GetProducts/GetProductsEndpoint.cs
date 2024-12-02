// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsRequest(int Page = 1, int PageSize = 10);

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
        
                var result = await sender.Send(query);
        
                var response = result.Adapt<GetProductsResponse>();
        
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get all products")
            .WithDescription("Get all products in the catalog");
    }
}