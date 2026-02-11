using Basket.Data.Repositories;

namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string Username) 
    : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

internal class DeleteBasketHandler(IBasketRepository basketRepository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(
        DeleteBasketCommand command,
        CancellationToken cancellationToken)
    {
        
        await basketRepository
            .DeleteBasketAsync(command.Username, cancellationToken);

        return new DeleteBasketResult(true);
    }
}
