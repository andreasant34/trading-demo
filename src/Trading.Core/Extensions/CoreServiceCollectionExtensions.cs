using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Interfaces;
using Trading.Core.Services;

namespace Trading.Core.Extensions
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection AddTradingCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITradingService, TradingService>();
            return services;
        }
    }
}
