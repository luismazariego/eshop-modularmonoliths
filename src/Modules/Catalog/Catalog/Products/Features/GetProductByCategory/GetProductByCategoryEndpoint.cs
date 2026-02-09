 
namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", 
            async (string category, ISender sender) =>
        {
            var query = new GetProductByCategoryQuery(category);
            var products = await sender.Send(query);
            var response = products.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Gets products by category")
        .WithDescription("Retrieves a list of products from the catalog by category");
    }
}
