namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string Username) 
    : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

internal class DeleteBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(
        DeleteBasketCommand command,
        CancellationToken cancellationToken)
    {
        var existingBasket = await dbContext.ShoppingCarts
            .SingleOrDefaultAsync(b => 
                b.Username == command.Username, 
                cancellationToken: cancellationToken) ?? 
            throw new BasketNotFoundException(command.Username);

        dbContext.ShoppingCarts.Remove(existingBasket);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteBasketResult(true);
    }
}
