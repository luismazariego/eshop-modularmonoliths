using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Data.Repositories;

public class CacheBasketRepository(
    IBasketRepository repository,
    IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> CreateBasketAsync(
        ShoppingCart basket,
        CancellationToken cancellationToken = default)
    {
        await repository.CreateBasketAsync(basket, cancellationToken);

        await cache.SetStringAsync(
            basket.Username,
            JsonSerializer.Serialize(basket),
            cancellationToken);

        return basket;
    }

    public async Task<bool> DeleteBasketAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasketAsync(username, cancellationToken);
        await cache.RemoveAsync(username, cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(
        string username,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        if (!asNoTracking)
        {
            return await repository
                .GetBasketAsync(username, asNoTracking, cancellationToken);
        }
        var cachedBasket = await cache
            .GetStringAsync(username, cancellationToken);

        if (!string.IsNullOrWhiteSpace(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;           
        }
        var basket = await repository
            .GetBasketAsync(username, asNoTracking, cancellationToken); 

        await cache.SetStringAsync(
            username,
            JsonSerializer.Serialize(basket),
            cancellationToken);

        return basket;
    }

    public async Task<int> SaveChangesAsync(
        string? username = null,
        CancellationToken cancellationToken = default)
    {
        var result = await repository
            .SaveChangesAsync(username, cancellationToken);

        if (username is not null)
        {
            await cache.RemoveAsync(username, cancellationToken);
        }

        return result;
    }
}
