namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) 
    : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);


internal class GetProductByCategoryHandler(CatalogDbContext dbContext)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(
        GetProductByCategoryQuery query, 
        CancellationToken cancellationToken)
    {
        var productDtos = await dbContext
            .Products
            .AsNoTracking()
            .Where(p => p.Category.Contains(query.Category))
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Category,
                p.Description,
                p.ImageFile,
                p.Price))
            .ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(productDtos);
    }
}
