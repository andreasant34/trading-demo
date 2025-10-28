using Moq;
using Trading.Core.Entities;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.Core.Tests.MockHelpers
{
    public static class MockTradeHelper
    {
        public static Mock<ITradeRepository> InitMockTradeRepositoryReadOnly(List<TradeEntity> tradeEntities)
        {
            var mockTradeRepository = new Mock<ITradeRepository>();

            mockTradeRepository
                .Setup(x => x.ListTradesByUserAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => tradeEntities.Where(x => x.UserId == id));

            mockTradeRepository
                .Setup(x => x.ListTradesByUserSecurityAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int userId, int securityId) => tradeEntities.Where(x => x.UserId == userId && x.SecurityId == securityId));

            return mockTradeRepository;
        }

        public static List<TradeEntity> GetTestTradeEntities()
        {
            var result = new List<TradeEntity>();

            for (var tradeIdCounter = 1; tradeIdCounter <= 10; tradeIdCounter++)
            {
                var userId = tradeIdCounter % 2 == 0 ? 1 : 2;

                result.Add(new TradeEntity
                {
                    Id = tradeIdCounter,
                    UserId = userId,
                    InvestmentAccountId = userId, // Let investment account id be equal to the user id
                    SecurityId = userId, // Let security id be equal to the user id
                    TransactionType = TransactionType.Buy,
                    Price = 10,
                    Quantity = 2,
                    CurrencyCode = "EUR",
                    TotalAmount = 20
                });
            }

            return result;
        }
    }
}
