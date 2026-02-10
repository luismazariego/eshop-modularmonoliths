using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsResponse(PaginatedResult<ProductDto> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
            [AsParameters] PaginationRequest request,
            ISender sender) =>
        {
            var products = await sender.Send(new GetProductsQuery(request));
            var response = products.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Gets a list of products")
        .WithDescription("Retrieves a list of products from the catalog");
    }
}
