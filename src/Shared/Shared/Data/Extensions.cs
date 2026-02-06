using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data;

public static class Extensions
{
    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseMigration<TContext>()
        where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(app.ApplicationServices)
                .GetAwaiter()
                .GetResult();

            SeedDataAsync(app.ApplicationServices)
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }

    private static async Task MigrateDatabaseAsync<TContext>(
        IServiceProvider serviceProvider) 
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope
            .ServiceProvider
            .GetRequiredService<TContext>();

        await dbContext.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var seeders = scope
            .ServiceProvider
            .GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAllAsync();
        }
    }
}
