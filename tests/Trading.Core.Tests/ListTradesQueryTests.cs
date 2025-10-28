using FluentValidation;
using Moq;
using Trading.Core.Entities;
using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;
using Trading.Core.Queries;
using Trading.Core.Tests.Comparers;

namespace Trading.Core.Tests
{
    public class ListTradesQueryTests
    {
        [Fact]
        public async Task ListTradesQueryHandler_ShouldReturnUserTradesOnly()
        {
            var tradeEntities = GetTestTradeEntities();
            var userIdToTest = tradeEntities.First().UserId;

            var mapper = MappingProfileTests.GetTestMapperConfigurationForAllProfiles().CreateMapper();
            var expectedResult = mapper.Map<IEnumerable<TradeDetails>>(tradeEntities.Where(x => x.UserId == userIdToTest)).ToList();

            var mockUserContextService = new Mock<IUserContextService>();
            mockUserContextService.Setup(x => x.GetUserId()).Returns(userIdToTest);

            var mockTradeRepository = new Mock<ITradeRepository>();
            mockTradeRepository
                .Setup(x => x.ListTradesByUserAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => tradeEntities.Where(x => x.UserId == id));

            var queryHandler = new ListTradesQueryHandler(mockUserContextService.Object,mockTradeRepository.Object,mapper);
            var result = await queryHandler.Handle(new ListTradesQuery(),CancellationToken.None);

            Assert.Equal(expectedResult, result,new TradeDetailsComparer());
        }

        private List<TradeEntity> GetTestTradeEntities()
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
                    TransactionType = Models.TransactionType.Buy,
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
