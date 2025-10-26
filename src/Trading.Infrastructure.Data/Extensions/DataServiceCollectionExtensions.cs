using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Interfaces.Data;
using Trading.Infrastructure.Data.Repositories;

namespace Trading.Infrastructure.Data.Extensions
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddTradingDataServices(this IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddMaps(typeof(DataServiceCollectionExtensions).Assembly));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISecurityRepository, SecurityRepository>();
            return services;
        }
    }
}