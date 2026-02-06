using Shared.Data.Seed;

namespace Catalog.Data.Seed;

internal class CatalogDataSeeder(CatalogDbContext dbContext) 
    : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if (!await dbContext.Products.AnyAsync())
        {
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
    }
}
