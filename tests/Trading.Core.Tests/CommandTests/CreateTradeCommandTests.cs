using AutoMapper;
using Moq;
using Trading.Core.Commands;
using Trading.Core.Entities;
using Trading.Core.Exceptions;
using Trading.Core.Interfaces.MessageBus;
using Trading.Core.Models;
using Trading.Core.Tests.MockHelpers;

namespace Trading.Core.Tests.CommandTests
{
    public class CreateTradeCommandTests
    {
        [Fact]
        public async Task CreateTradeCommand_ShouldThrowInvestmentAccountNotFound_WhenInvestmentAccountIsInvalid()
        {
            InitTradeTest(out var createTradeCommandHandler, out var mapper, out var userIdToTest, out var tradeEntities, out var userEntities, out var securityEntities);

            var tradeEntity = tradeEntities.First(x => x.UserId == userIdToTest);

            var createTradeCopy = mapper.Map<CreateTradeCommand>(tradeEntity);

            //Assign an investment account of another user
            createTradeCopy.InvestmentAccountId = userEntities.First(x => x.Id != userIdToTest).InvestmentAccounts.First().Id;

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await createTradeCommandHandler.Handle(createTradeCopy, CancellationToken.None));
            Assert.Equal(ErrorCode.INVESTMENT_ACCOUNT_NOT_FOUND, exception.ErrorCode);
        }

        [Fact]
        public async Task CreateTradeCommand_ShouldThrowSecurityNotFound_WhenSecurityIsInvalid()
        {
            InitTradeTest(out var createTradeCommandHandler, out var mapper, out var userIdToTest, out var tradeEntities, out var userEntities, out var securityEntities);

            var tradeEntity = tradeEntities.First(x => x.UserId == userIdToTest);

            var createTradeCopy = mapper.Map<CreateTradeCommand>(tradeEntity);

            //Assign an invalid security id
            createTradeCopy.SecurityId = securityEntities.Select(x => x.Id).Max() + 1;

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await createTradeCommandHandler.Handle(createTradeCopy, CancellationToken.None));
            Assert.Equal(ErrorCode.SECURITY_NOT_FOUND, exception.ErrorCode);
        }

        [Fact]
        public async Task CreateTradeCommand_ShouldThrowTradeSellQuantityNotAvailable_WhenQuantityNotAvailable()
        {
            InitTradeTest(out var createTradeCommandHandler, out var mapper, out var userIdToTest, out var tradeEntities, out var userEntities, out var securityEntities);

            var tradeEntity = tradeEntities.First(x => x.UserId == userIdToTest);

            var createTradeCopy = mapper.Map<CreateTradeCommand>(tradeEntity);

            var tradesOfThisUserSecurity = tradeEntities.Where(x => x.UserId == userIdToTest && x.SecurityId == createTradeCopy.SecurityId);
            var buyQuantity = tradesOfThisUserSecurity.Where(x => x.TransactionType == TransactionType.Buy).Sum(x => x.Quantity);
            var sellQuantity = tradesOfThisUserSecurity.Where(x => x.TransactionType == TransactionType.Sell).Sum(x => x.Quantity);
            var availableQuantity = buyQuantity - sellQuantity;

            //Attempt to sell more securities than the user has available
            createTradeCopy.TransactionType = TransactionType.Sell;
            createTradeCopy.Quantity = availableQuantity + 1;

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await createTradeCommandHandler.Handle(createTradeCopy, CancellationToken.None));
            Assert.Equal(ErrorCode.TRADE_SELL_QUANTITY_NOT_AVAILABLE, exception.ErrorCode);
        }

        [Fact]
        public async Task CreateTradeCommand_ShouldSucceedSell_WhenQuantityIsAvailable()
        {
            InitTradeTest(out var createTradeCommandHandler, out var mapper, out var userIdToTest, out var tradeEntities, out var userEntities, out var securityEntities);

            var buyTradeEntity = tradeEntities.First(x => x.UserId == userIdToTest && x.TransactionType == TransactionType.Buy);

            var createTradeCopy = mapper.Map<CreateTradeCommand>(buyTradeEntity);

            //Attempt to sell a previous buy
            createTradeCopy.TransactionType = TransactionType.Sell;

            var newTradeId = await createTradeCommandHandler.Handle(createTradeCopy, CancellationToken.None);
            Assert.True(newTradeId > 0);
        }

        public static void InitTradeTest(out CreateTradeCommandHandler commandHandler, out IMapper mapper, out int userIdToTest, out List<TradeEntity> tradeEntities, out List<UserEntity> userEntities, out List<SecurityEntity> securityEntities)
        {
            userEntities = MockUserHelper.GetTestUserEntities();
            tradeEntities = MockTradeHelper.GetTestTradeEntities();

            userIdToTest = tradeEntities.First().UserId;
            mapper = MappingProfileTests.GetTestMapperConfigurationForAllProfiles().CreateMapper();

            var mockUserContextService = MockUserHelper.InitMockUserContextService(userIdToTest);
            var mockUserRepository = MockUserHelper.InitMockUserRepository(userEntities);

            var mockTradeRepository = MockTradeHelper.InitMockTradeRepositoryReadOnly(tradeEntities);
            _ = mockTradeRepository
                .Setup(x => x.CreateTradeAsync(It.IsAny<TradeEntity>()))
                .ReturnsAsync(1);

            securityEntities = MockSecurityHelper.GetTestSecurityEntities();
            var mockSecurityRepository = MockSecurityHelper.InitMockSecurityRepository(securityEntities);

            var mockMessageBus = new Mock<IMessageBus>();
            _ = mockMessageBus
                .Setup(x => x.PublishAsync(It.IsAny<It.IsAnyType>))
                .Returns(Task.CompletedTask);

            commandHandler = new CreateTradeCommandHandler(
                mockUserContextService.Object,
                mockTradeRepository.Object,
                mockSecurityRepository.Object,
                mockUserRepository.Object,
                mapper,
                mockMessageBus.Object
            );
        }
    }
}
