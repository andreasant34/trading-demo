using Microsoft.EntityFrameworkCore;
using Trading.Core.Entities;

namespace Trading.Infrastructure.Data.Tests
{
    internal class SeededTradingDbContext : TradingDbContext
    {
        public ISet<int> SeededUserIDs { get; private set; }
        public ISet<string> SeededEmails { get; private set; }

        public SeededTradingDbContext(DbContextOptions<TradingDbContext> dbContextOptions) 
            : base(dbContextOptions)
        {
            SeededUserIDs = new HashSet<int>();
            SeededEmails = new HashSet<string>();
        }

        public void SeedAllData()
        {
            SeedUserData();
        }

        public void SeedUserData()
        {
            int investmentAccountIdCounter = 1;
            for (var userIdCounter = 1; userIdCounter <= 5; userIdCounter++)
            {
                var newUser = new UserEntity
                {
                    Id = userIdCounter,
                    Name = $"Name {userIdCounter}",
                    Surname = $"Surname {userIdCounter}",
                    Email = $"name{userIdCounter}@gmail.com",
                    InvestmentAccounts = new List<InvestmentAccountEntity>()
                };

                SeededUserIDs.Add(newUser.Id);
                SeededEmails.Add(newUser.Email);
                Users.Add(newUser);

                for (var investmentAccountCounter = 1; investmentAccountCounter <= 2; investmentAccountCounter++)
                {
                    var investmentAccount = new InvestmentAccountEntity
                    {
                        Id = investmentAccountIdCounter,
                        Name = $"User {userIdCounter} Investment Account {investmentAccountCounter}",
                        UserId = userIdCounter
                    };

                    InvestmentAccounts.Add(investmentAccount);
                    investmentAccountIdCounter++;
                }

            }

            SaveChanges();
        }
    }
}
