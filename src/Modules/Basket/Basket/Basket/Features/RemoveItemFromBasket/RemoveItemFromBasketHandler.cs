namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(string Username, Guid ProductId) 
    : ICommand<RemoveItemFromBasketResult>;

public record RemoveItemFromBasketResult(Guid Id);

public class RemoveItemFromBasketValidator 
    : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.");
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");
    }
}

public class RemoveItemFromBasketHandler(BasketDbContext dbContext) 
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(
        RemoveItemFromBasketCommand command,
        CancellationToken cancellationToken)
    {
        var basket = await dbContext.ShoppingCarts
            .Include(b => b.Items)
            .SingleOrDefaultAsync(b => 
                b.Username == command.Username, 
                cancellationToken: cancellationToken) ?? 
            throw new BasketNotFoundException(command.Username);

        basket.RemoveItem(command.ProductId);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new RemoveItemFromBasketResult(basket.Id);
    }
}
