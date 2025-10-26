using Microsoft.Extensions.DependencyInjection;
using Trading.Core.Interfaces;
using Trading.Core.Services;

namespace Trading.Core.Extensions
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection AddTradingCoreServices(this IServiceCollection services)
        {
            _ = services.AddScoped<ITradingService, TradingService>();
            return services;
        }
    }
}
