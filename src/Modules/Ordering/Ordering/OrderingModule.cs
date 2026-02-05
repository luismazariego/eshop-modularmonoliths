using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering;

public static class OrderingModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOrderingModule(
        IConfiguration configuration)
        {
            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseOrderingModule()
        {
            return app;
        }
    }
}
