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

            #region To be removed once a dockerized instance is implemented
            modelBuilder.Entity<SecurityEntity>().HasData( new SecurityEntity
            {
                Id = 1,
                Name = "S&P 500"
            });

            modelBuilder.Entity<SecurityEntity>().HasData( new SecurityEntity
            {
                Id = 2,
                Name = "Apple"
            });

            modelBuilder.Entity<SecurityEntity>().HasData(new SecurityEntity
            {
                Id = 3,
                Name = "Samsung"
            });

            var userEntity = new UserEntity
            {
                Id = 1,
                Name = "Test Name",
                Surname = "Test Surname",
                Email = "test@gmail.com"
            };
            modelBuilder.Entity<UserEntity>().HasData(userEntity);

            modelBuilder.Entity<InvestmentAccountEntity>().HasData(new InvestmentAccountEntity 
            { 
                UserId = userEntity.Id,
                Id = 1,
                Name = "Investment Account A" 
            });
            #endregion
        }
    }
}
