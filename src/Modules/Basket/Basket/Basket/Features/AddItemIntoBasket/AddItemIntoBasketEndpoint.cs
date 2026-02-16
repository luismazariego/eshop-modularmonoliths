namespace Basket.Basket.Features.AddItemIntoBasket;

public record AddItemIntoBasketRequest(ShoppingCartItemDto Item);
public record AddItemIntoBasketResponse(Guid Id);

public class AddItemIntoBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/basket/{username}/items",
            async (
                [FromRoute] string username, 
                [FromBody]AddItemIntoBasketRequest request, 
                ISender sender) =>
            {
                var command = new AddItemIntoBasketCommand(username, request.Item);
                var result = await sender.Send(command);
                return Results.Created(
                    $"/basket/{username}/items/{result.Id}", 
                    new AddItemIntoBasketResponse(result.Id));
            })
            .Produces<AddItemIntoBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Add Item to Basket")
            .WithDescription("Add a new item to the shopping basket")
            .RequireAuthorization();
    }
}
