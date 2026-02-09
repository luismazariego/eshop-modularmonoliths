namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) 
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Product.Category)
            .NotEmpty();
        RuleFor(x => x.Product.ImageFile)
            .NotEmpty();
        RuleFor(x => x.Product.Price)
            .GreaterThan(0);
    }
}

internal sealed class CreateProductHandler(
    CatalogDbContext dbContext,
    ILogger<CreateProductHandler> logger) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Creating new product with name: {ProductName}", 
            command.Product.Name);

        var product = CreateNewProduct(command.Product);
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            productDto.Name,
            productDto.Category,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price);

        return product;
    }
}
