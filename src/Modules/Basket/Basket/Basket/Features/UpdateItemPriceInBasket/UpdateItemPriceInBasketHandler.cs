namespace Basket.Basket.Features.UpdateItemPriceInBasket;

public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal Price)
    : ICommand<UpdateItemPriceInBasketResult>;

public record UpdateItemPriceInBasketResult(bool IsSuccess);

public class UpdateItemPriceInBasketCommandValidator
    : AbstractValidator<UpdateItemPriceInBasketCommand>  
{
    public UpdateItemPriceInBasketCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

public class UpdateItemPriceInBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<
        UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
{
    public async Task<UpdateItemPriceInBasketResult> Handle(
        UpdateItemPriceInBasketCommand command,
        CancellationToken cancellationToken)
    {
        var itemsToUpdate = await dbContext.ShoppingCartItems
            .Where(item => item.ProductId == command.ProductId)
            .ToListAsync(cancellationToken);

        if (itemsToUpdate.Count == 0)
        {
            return new UpdateItemPriceInBasketResult(IsSuccess: false);
        }

        foreach (var item in itemsToUpdate)
        {
            item.UpdatePrice(command.Price);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateItemPriceInBasketResult(IsSuccess: true);
    }
}
