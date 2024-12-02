// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics.CodeAnalysis;

namespace Catalog.Api.Products.GetProductByCategory;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record GetCategoryByCategoryResult(IEnumerable<Product> Products);

public class GetCategoryByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var query = new GetProductsByCategoryQuery(category);
        
                var result = await sender.Send(query);
        
                var response = result.Adapt<GetCategoryByCategoryResult>();

                return Results.Ok(response);
            })
            .WithName("GetCategoryByCategory")
            .Produces<GetCategoryByCategoryResult>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get a category by category")
            .WithDescription("Get a category by its unique identifier");
    }
}