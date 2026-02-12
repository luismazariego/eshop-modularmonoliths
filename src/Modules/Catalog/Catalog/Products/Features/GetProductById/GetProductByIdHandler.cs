namespace Catalog.Products.Features.GetProductById;

internal class GetProductByIdHandler(CatalogDbContext dbContext)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .Where(p => p.Id == request.Id)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Category,
                p.Description,
                p.ImageFile,
                p.Price))
            .FirstOrDefaultAsync(cancellationToken);

        return product is null ?
            throw new ProductNotFoundException(request.Id) : 
            new GetProductByIdResult(product);
    }
}
