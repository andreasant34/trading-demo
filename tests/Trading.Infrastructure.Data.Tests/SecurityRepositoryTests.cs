using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.Data.Repositories;

namespace Trading.Infrastructure.Data.Tests
{
    public class SecurityRepositoryTests
    {
        [Fact]
        public async Task ListSecurities_ShouldReturnAllSecurities()
        {
            var context = InitSeededTradingDbContext();
            var securityRepository = new SecurityRepository(context);

            var allSecurities = await securityRepository.ListSecuritiesAsync();
            Assert.Equal(allSecurities.Count(), context.SeededSecurityIDs.Count());
        }

        [Fact]
        public async Task GetSecurityById_ShouldReturnSecurity_When_Security_Exists()
        {
            var context = InitSeededTradingDbContext();
            var securityRepository = new SecurityRepository(context);

            var existingSecurity = await securityRepository.GetSecurityByIdAsync(context.SeededSecurityIDs.First());
            Assert.NotNull(existingSecurity);
        }

        [Fact]
        public async Task GetSecurityById_ShouldReturnNull_When_Security_NotExists()
        {
            var context = InitSeededTradingDbContext();
            var securityRepository = new SecurityRepository(context);

            var notFoundSecurity = await securityRepository.GetSecurityByIdAsync(context.SeededSecurityIDs.Max() + 1);
            Assert.Null(notFoundSecurity);
        }

        private static SeededTradingDbContext InitSeededTradingDbContext()
        {
            var options = new DbContextOptionsBuilder<TradingDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;

            var context = new SeededTradingDbContext(options);
            context.Database.EnsureCreated();
            context.SeedSecurityData();
            return context;
        }
    }
}
