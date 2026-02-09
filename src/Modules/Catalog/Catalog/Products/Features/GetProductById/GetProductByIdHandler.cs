namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdRequest(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(ProductDto Product);

internal class GetProductByIdHandler(CatalogDbContext dbContext)
    : IQueryHandler<GetProductByIdRequest, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdRequest request,
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
            throw new Exception($"Product with id {request.Id} not found.") : 
            new GetProductByIdResult(product);
    }
}
