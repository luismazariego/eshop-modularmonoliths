namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketResponse(Guid Id);

public class RemoveItemFromBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/basket/{username}/items/{productId}",
            async (
                [FromRoute] string username,
                [FromRoute] Guid productId,
                ISender sender) =>
            {
                var command = new RemoveItemFromBasketCommand(username, productId);
                var result = await sender.Send(command);
                return Results.Ok(new RemoveItemFromBasketResponse(result.Id));
            })
            .Produces<RemoveItemFromBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Remove Item from Basket")
            .WithDescription("Remove an item from the shopping basket")
            .RequireAuthorization();
    }
}
