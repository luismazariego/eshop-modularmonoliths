namespace Basket.Basket.Features.GetBasket;

public record GetBasketResponse(ShoppingCartDto ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/basket/{username}",
            async (string username, ISender sender) =>
            {
                var query = new GetBasketQuery(username);
                var result = await sender.Send(query);
                return Results.Ok(new GetBasketResponse(result.ShoppingCart));
            })
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Basket")
            .WithDescription("Retrieve the shopping basket for a specific user");
    }
}
