using Basket.Data.Repositories;

namespace Basket.Basket.Features.AddItemIntoBasket;

public record AddItemIntoBasketCommand(
    string Username, 
    ShoppingCartItemDto ShoppingCartItem) : ICommand<AddItemIntoBasketResult>;

public record AddItemIntoBasketResult(Guid Id);

public class AddItemIntoBasketValidator : AbstractValidator<AddItemIntoBasketCommand>
{
    public AddItemIntoBasketValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.");
        When(x => x.ShoppingCartItem != null, () =>
        {
            RuleFor(x => x.ShoppingCartItem.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required.");
            RuleFor(x => x.ShoppingCartItem.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        });
    }
}

public class AddItemIntoBasketHandler(IBasketRepository basketRepository) 
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    public async Task<AddItemIntoBasketResult> Handle(
        AddItemIntoBasketCommand command, 
        CancellationToken cancellationToken)
    {
        var shoppingCart = await basketRepository
            .GetBasketAsync(command.Username, false, cancellationToken);

        shoppingCart.AddItem(
            command.ShoppingCartItem.ProductId, 
            command.ShoppingCartItem.Quantity, 
            command.ShoppingCartItem.Color, 
            command.ShoppingCartItem.Price, 
            command.ShoppingCartItem.ProductName);

        await basketRepository.SaveChangesAsync(command.Username, cancellationToken);

        return new AddItemIntoBasketResult(shoppingCart.Id);
    }
}
