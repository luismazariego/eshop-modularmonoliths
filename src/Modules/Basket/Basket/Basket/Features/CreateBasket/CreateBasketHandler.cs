namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCart) 
    : ICommand<CreateBasketResult>;

public record CreateBasketResult(Guid Id);

public class CreateBasketValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator()
    {
        RuleFor(x => x.ShoppingCart.Username).NotNull();
    }
}


public class CreateBasketHandler(BasketDbContext dbContext) 
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(
        CreateBasketCommand command,
        CancellationToken cancellationToken)
    {
        var shoppingCart = CreateNewBasket(command.ShoppingCart);
        dbContext.ShoppingCarts.Add(shoppingCart);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBasketResult(shoppingCart.Id);
    }

    private static ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
    {
        var shoppingCart = ShoppingCart
                .Create(Guid.NewGuid(), shoppingCartDto.Username);

        shoppingCartDto.Items.ForEach(item =>
        {
            shoppingCart.AddItem(
                item.ProductId,
                item.Quantity,
                item.Color,
                item.Price,
                item.ProductName);
        });

        return shoppingCart;
    }
}


