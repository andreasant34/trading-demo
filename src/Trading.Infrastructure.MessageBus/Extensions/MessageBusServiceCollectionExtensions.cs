using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trading.Core.Interfaces.MessageBus;
using Trading.Infrastructure.MessageBus.Consumers;

namespace Trading.Infrastructure.MessageBus.Extensions
{
    public static class MessageBusServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageBusServices(this IServiceCollection services, IConfiguration configuration, bool isConsumer)
        {
            services.AddMassTransit(x =>
            {
                if (isConsumer)
                {
                    x.AddConsumer<TradeCreatedConsumer>();
                }

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetSection("RabbitMq:Host").Value!, h => 
                    {
                        h.Username(configuration.GetSection("RabbitMq:Username").Value!);
                        h.Password(configuration.GetSection("RabbitMq:Password").Value!);
                    });

                    if (isConsumer)
                    {
                        cfg.ReceiveEndpoint("trade-notification-queue", e =>
                        {
                            e.ConfigureConsumer<TradeCreatedConsumer>(context);
                        });
                    }

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IMessageBus, TradingMessageBus>();

            return services;
        }
    }
}
