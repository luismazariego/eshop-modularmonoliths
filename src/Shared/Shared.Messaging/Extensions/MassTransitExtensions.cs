using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Messaging.Extensions;

public static class MassTransitExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddMassTransitWithAssemblies(
            IConfiguration configuration, 
            params Assembly[] assemblies)
        {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                config.SetInMemorySagaRepositoryProvider();
                config.AddConsumers(assemblies);
                config.AddSagas(assemblies);
                config.AddActivities(assemblies);

                config.UsingRabbitMq((context, cfg) =>
                {
                    var host = configuration["MessageBroker:Host"]!;
                    cfg.Host(host, h =>
                    {
                        h.Username(configuration["MessageBroker:Username"]!);
                        h.Password(configuration["MessageBroker:Password"]!);
                    });
                    cfg.ConfigureEndpoints(context);
                });

                //config.UsingInMemory((context, cfg) =>
                //{
                //    cfg.ConfigureEndpoints(context);
                //});
            });

            return services;
        }
    }
}
