using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDto> Products);

internal sealed class GetProductsHandler(CatalogDbContext dbContext) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery query, 
        CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext
            .Products
            .AsNoTracking()
            .LongCountAsync(cancellationToken);

        // better to use projection here to avoid loading unnecessary data into memory
        // No use of mapster here.
        var productDtos = await dbContext
            .Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Category,
                p.Description,
                p.ImageFile,
                p.Price))
            .ToListAsync(cancellationToken);

        return new GetProductsResult(new PaginatedResult<ProductDto>(
            pageIndex, 
            pageSize, 
            totalCount, 
            productDtos));
    }
}
