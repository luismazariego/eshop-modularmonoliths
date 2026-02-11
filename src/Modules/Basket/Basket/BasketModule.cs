using Basket.Data.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Basket;

public static class BasketModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBasketModule(IConfiguration configuration)
        {
            // Application services

            // Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Decorate<IBasketRepository, CacheBasketRepository>();
            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseBasketModule()
        {
            app.UseMigration<BasketDbContext>();
            return app;
        }
    }
}
                                                                                         