namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketRequest(ShoppingCartDto ShoppingCart);

public record CreateBasketResponse(Guid Id);

public class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/basket",
            async (CreateBasketRequest request, ISender sender) =>
        {
            var command = new CreateBasketCommand(request.ShoppingCart);
            var result = await sender.Send(command);
            return Results.Created(
                $"/basket/{result.Id}", 
                new CreateBasketResponse(result.Id));
        })
        .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Basket")
        .WithDescription("Create a new shopping basket");
    }
}
