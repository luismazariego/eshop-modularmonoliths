namespace Basket.Data.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasketAsync(
        string username,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<ShoppingCart> CreateBasketAsync(
        ShoppingCart basket,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteBasketAsync(
        string username,
        CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(
        string? username = null,
        CancellationToken cancellationToken = default);
}
