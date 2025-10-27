using Microsoft.EntityFrameworkCore;
using Trading.Core.Entities;

namespace Trading.Infrastructure.Data
{
    public class TradingDbContext : DbContext
    {
        public TradingDbContext(DbContextOptions<TradingDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SecurityEntity> Securities { get; set; }
        public DbSet<TradeEntity> Trades { get; set; }
        public DbSet<InvestmentAccountEntity> InvestmentAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
