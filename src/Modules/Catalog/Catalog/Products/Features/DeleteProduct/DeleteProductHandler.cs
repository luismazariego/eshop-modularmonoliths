namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) 
    : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

internal sealed class DeleteProductHandler(CatalogDbContext dbContext) 
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(
        DeleteProductCommand command, 
        CancellationToken cancellationToken)
    {
        var product = await dbContext
            .Products
            .FindAsync([command.ProductId], cancellationToken) ?? 
            throw new Exception($"Product with id {command.ProductId} not found.");

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
