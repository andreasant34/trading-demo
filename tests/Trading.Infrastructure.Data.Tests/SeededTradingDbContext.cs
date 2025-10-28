using Microsoft.EntityFrameworkCore;
using Trading.Core.Entities;

namespace Trading.Infrastructure.Data.Tests
{
    internal class SeededTradingDbContext : TradingDbContext
    {
        public IList<UserEntity> SeededUsers { get; private set; }
        public IList<InvestmentAccountEntity> SeededInvestmentAccounts { get; private set; }
        public IList<SecurityEntity> SeededSecurities { get; private set; }
        public IList<TradeEntity> SeededTrades { get; private set; }

        public SeededTradingDbContext(DbContextOptions<TradingDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
            SeededUsers = [];
            SeededInvestmentAccounts = [];
            SeededSecurities = [];
            SeededTrades = [];
        }

        public void SeedAllData()
        {
            SeedUserData();
            SeedSecurityData();
            SeedTradeData();
        }

        public void SeedUserData()
        {
            var investmentAccountIdCounter = 1;
            for (var userIdCounter = 1; userIdCounter <= 5; userIdCounter++)
            {
                var newUser = new UserEntity
                {
                    Id = userIdCounter,
                    Name = $"Name {userIdCounter}",
                    Surname = $"Surname {userIdCounter}",
                    Email = $"name{userIdCounter}@gmail.com",
                    InvestmentAccounts = []
                };

                SeededUsers.Add(newUser);
                _ = Users.Add(newUser);

                for (var investmentAccountCounter = 1; investmentAccountCounter <= 2; investmentAccountCounter++)
                {
                    var investmentAccount = new InvestmentAccountEntity
                    {
                        Id = investmentAccountIdCounter,
                        Name = $"User {userIdCounter} Investment Account {investmentAccountCounter}",
                        UserId = userIdCounter
                    };

                    SeededInvestmentAccounts.Add(investmentAccount);
                    _ = InvestmentAccounts.Add(investmentAccount);
                    investmentAccountIdCounter++;
                }
            }

            _ = SaveChanges();
        }

        public void SeedSecurityData()
        {
            for (var securityIdCounter = 1; securityIdCounter <= 5; securityIdCounter++)
            {
                var newSecurity = new SecurityEntity
                {
                    Id = securityIdCounter,
                    Name = $"Security {securityIdCounter}"
                };

                SeededSecurities.Add(newSecurity);
                _ = Securities.Add(newSecurity);
            }

            _ = SaveChanges();
        }

        public void SeedTradeData()
        {
            var lkpInvestmentAccountsByUser = SeededInvestmentAccounts.ToLookup(x => x.UserId);

            var tradeIdCounter = 1;

            foreach (var user in SeededUsers)
            {
                var userInvestmentAccounts = lkpInvestmentAccountsByUser[user.Id];
                foreach (var security in SeededSecurities)
                {
                    var securityPrice = tradeIdCounter;
                    var quantity = tradeIdCounter;

                    var newTrade = new TradeEntity
                    {
                        Id = tradeIdCounter,
                        UserId = user.Id,
                        InvestmentAccountId = userInvestmentAccounts.First().Id,
                        SecurityId = security.Id,
                        TransactionType = Core.Models.TransactionType.Buy,
                        Price = securityPrice,
                        Quantity = quantity,
                        CurrencyCode = "EUR",
                        TotalAmount = securityPrice * quantity
                    };

                    SeededTrades.Add(newTrade);
                    _ = Trades.Add(newTrade);
                    tradeIdCounter++;
                }
            }

            _ = SaveChanges();
        }
    }
}
