namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/basket/{username}",
            async (string username, ISender sender) =>
            {
                var command = new DeleteBasketCommand(username);
                var result = await sender.Send(command);
                return Results.Ok(new DeleteBasketResponse(result.IsSuccess));
            })
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Basket")
            .WithDescription("Delete an existing shopping basket");
    }
}
