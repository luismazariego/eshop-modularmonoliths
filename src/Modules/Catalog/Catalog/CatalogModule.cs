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
            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseCatalogModule()
        {
            return app;
        }
    }
}
