using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog;

public static class CatalogModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCatalogModule(
        IConfiguration configuration)
        {
            // Api endpoints services

            // Application services

            // Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");
            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                // add interceptors, logging, etc. as needed
            });
            services.AddScoped<IDataSeeder, CatalogDataSeeder>();
            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseCatalogModule()
        {
            app.UseMigration<CatalogDbContext>();
            return app;
        }
    }
}
