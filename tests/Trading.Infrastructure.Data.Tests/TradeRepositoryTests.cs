using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.Data.Repositories;

namespace Trading.Infrastructure.Data.Tests
{
    public class TradeRepositoryTests
    {
        [Fact]
        public async Task ListTradesByUser_ShouldReturnAllTradesOfThisUserOnly()
        {
            var context = InitSeededTradingDbContext();
            var tradeRepository = new TradeRepository(context);

            var firstTrade = context.SeededTrades.First();
            var expectedUserTradeIds = context.SeededTrades.Where(x => x.UserId == firstTrade.UserId)
                .Select(x => x.Id).OrderBy(x => x);

            var allUserTrades = await tradeRepository.ListTradesByUserAsync(firstTrade.UserId);
            var actualUserTradeIds = allUserTrades.Select(x => x.Id).OrderBy(x => x);

            Assert.True(expectedUserTradeIds.SequenceEqual(actualUserTradeIds));
        }

        [Fact]
        public async Task ListTradesByUserSecurity_ShouldReturnAllTradesOfThisUserSecurityOnly()
        {
            var context = InitSeededTradingDbContext();
            var tradeRepository = new TradeRepository(context);

            var firstTrade = context.SeededTrades.First();
            var expectedUserSecurityTradeIds = context.SeededTrades.Where(x => x.UserId == firstTrade.UserId && x.SecurityId == firstTrade.SecurityId)
                .Select(x => x.Id).OrderBy(x => x);

            var allUserTrades = await tradeRepository.ListTradesByUserSecurityAsync(firstTrade.UserId, firstTrade.SecurityId);
            var actualUserSecurityTradeIds = allUserTrades.Select(x => x.Id).OrderBy(x => x);

            Assert.True(expectedUserSecurityTradeIds.SequenceEqual(actualUserSecurityTradeIds));
        }

        [Fact]
        public async Task CreateTrade_ShouldReturnUniqueTradeID()
        {
            var context = InitSeededTradingDbContext();
            var tradeRepository = new TradeRepository(context);

            var seededTradeIds = context.SeededTrades.Select(x => x.Id).ToHashSet();

            var tradeCopy = context.SeededTrades.First();
            tradeCopy.Id = 0;//Reset the ID

            var actualNewTradeId = await tradeRepository.CreateTradeAsync(tradeCopy);

            Assert.True(!seededTradeIds.Contains(actualNewTradeId));
        }

        private static SeededTradingDbContext InitSeededTradingDbContext()
        {
            var options = new DbContextOptionsBuilder<TradingDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;

            var context = new SeededTradingDbContext(options);
            _ = context.Database.EnsureCreated();
            context.SeedAllData();
            return context;
        }
    }
}
