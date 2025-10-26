using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data
{
    public class TradingDbContext:DbContext
    {
        public TradingDbContext(DbContextOptions<TradingDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<InvestmentAccount> InvestmentAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
