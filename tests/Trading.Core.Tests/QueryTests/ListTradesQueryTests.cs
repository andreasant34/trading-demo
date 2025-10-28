using FluentValidation;
using Moq;
using Trading.Core.Entities;
using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;
using Trading.Core.Queries;
using Trading.Core.Tests.Comparers;
using Trading.Core.Tests.MockHelpers;

namespace Trading.Core.Tests.QueryTests
{
    public class ListTradesQueryTests
    {
        [Fact]
        public async Task ListTradesQuery_ShouldReturnUserTradesOnly()
        {
            var tradeEntities = MockTradeHelper.GetTestTradeEntities();
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
    }
}
