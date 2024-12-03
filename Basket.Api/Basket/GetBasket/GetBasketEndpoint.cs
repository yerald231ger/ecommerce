// ReSharper disable ClassNeverInstantiated.Global

namespace Basket.Api.Basket.GetBasket;

// public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var query = new GetBasketQuery(userName);
            
            var result = await sender.Send(query);
            
            var response = result.Adapt<GetBasketResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get basket")
        .WithDescription("Get basket for the user");
    }
}