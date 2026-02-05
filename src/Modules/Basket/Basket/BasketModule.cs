using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket;

public static class BasketModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBasketModule(
        IConfiguration configuration)
        {
            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseBasketModule()
        {
            return app;
        }
    }
}
