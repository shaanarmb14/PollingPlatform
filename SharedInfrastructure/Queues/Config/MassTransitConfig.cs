using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Infrastructure.Queues.Config;

public static class MassTransitConfig
{
    private static readonly string VirtualHost = "/";
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, RabbitMQSettings settings)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(settings.Host, VirtualHost, h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }

    public static IServiceCollection AddMassTransitConsumerWithRabbitMq(this IServiceCollection services, Assembly assembly)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(assembly);
            x.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMQSettings>>().Value;
                cfg.Host(rabbitMqSettings.Host, h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });

        });

        return services;
    }
}
