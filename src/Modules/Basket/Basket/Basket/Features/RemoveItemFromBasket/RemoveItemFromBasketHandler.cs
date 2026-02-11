using Basket.Data.Repositories;

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

public class RemoveItemFromBasketHandler(IBasketRepository basketRepository) 
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(
        RemoveItemFromBasketCommand command,
        CancellationToken cancellationToken)
    {
        var basket = await basketRepository
            .GetBasketAsync(command.Username, false, cancellationToken);

        basket.RemoveItem(command.ProductId);

        await basketRepository
            .SaveChangesAsync(command.Username, cancellationToken);

        return new RemoveItemFromBasketResult(basket.Id);
    }
}
