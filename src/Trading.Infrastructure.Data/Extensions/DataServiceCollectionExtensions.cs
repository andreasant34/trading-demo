using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trading.Core.Interfaces.Data;
using Trading.Infrastructure.Data.Repositories;

namespace Trading.Infrastructure.Data.Extensions
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddTradingDataServices(this IServiceCollection services, Action<DbContextOptionsBuilder> dbOptionsAction)
        {
            _ = services.AddDbContext<TradingDbContext>(dbOptionsAction);
            _ = services.AddScoped<IUserRepository, UserRepository>();
            _ = services.AddScoped<ISecurityRepository, SecurityRepository>();
            _ = services.AddScoped<ITradeRepository, TradeRepository>();
            return services;
        }
    }
}