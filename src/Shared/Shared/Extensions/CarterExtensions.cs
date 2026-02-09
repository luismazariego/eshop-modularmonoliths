using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions;

public static class CarterExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCarterWithAssemblies(
            params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {
                foreach (var assembly in assemblies)
                {
                    var modules = assembly.GetTypes()
                        .Where(t => t.IsAssignableTo(typeof(ICarterModule)))
                        .ToArray();

                    config.WithModules(modules); 
                }
            });
            return services;
        }
    }
}
