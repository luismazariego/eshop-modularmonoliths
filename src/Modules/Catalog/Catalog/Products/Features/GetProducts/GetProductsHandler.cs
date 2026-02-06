namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductDto> Products);

internal sealed class GetProductsHandler(CatalogDbContext dbContext) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery query, 
        CancellationToken cancellationToken)
    {
        // better to use projection here to avoid loading unnecessary data into memory
        // No use of mapster here.
        var productDtos = await dbContext
            .Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Category,
                p.Description,
                p.ImageFile,
                p.Price))
            .ToListAsync(cancellationToken);

        return new GetProductsResult(productDtos);
    }
}
