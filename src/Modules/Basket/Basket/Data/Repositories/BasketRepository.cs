namespace Basket.Data.Repositories;

public class BasketRepository(BasketDbContext dbContext) : IBasketRepository
{
    public async Task<ShoppingCart> CreateBasketAsync(
        ShoppingCart basket,
        CancellationToken cancellationToken = default)
    {
        await dbContext.ShoppingCarts.AddAsync(basket, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasketAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        var basket = await GetBasketAsync(
            username,
            asNoTracking: false,
            cancellationToken);

        dbContext.ShoppingCarts.Remove(basket);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(
        string username,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.ShoppingCarts
            .Include(x => x.Items)  
            .Where(x => x.Username == username);
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        var basket = await query.SingleOrDefaultAsync(cancellationToken);

        return basket ??
            throw new BasketNotFoundException(username);
    }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
