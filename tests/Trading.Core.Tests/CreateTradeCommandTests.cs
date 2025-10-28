using AutoMapper;
using Moq;
using Trading.Core.Commands;
using Trading.Core.Entities;
using Trading.Core.Exceptions;
using Trading.Core.Interfaces.MessageBus;
using Trading.Core.Tests.MockHelpers;

namespace Trading.Core.Tests
{
    public class CreateTradeCommandTests
    {
        [Fact]
        public async Task CreateTradeCommand_ShouldThrowInvestmentAccountNotFound_WhenInvestmentAccountIsInvalid()
        {
            InitTradeTest(out var createTradeCommandHandler, out var mapper, out var userIdToTest, out var tradeEntities, out var userEntities, out var securityEntities);

            var firstTrade = tradeEntities.First();
            var firstTradeUser = userEntities.First( x=> x.Id == firstTrade.UserId);

            var createTradeCopy = mapper.Map<CreateTradeCommand>(firstTrade);
            //Assign an investment account of another user
            createTradeCopy.InvestmentAccountId = userEntities.First(x => x.Id != firstTrade.UserId).InvestmentAccounts.First().Id;

            var exception = await Assert.ThrowsAsync<BadRequestException>(async() => await createTradeCommandHandler.Handle(createTradeCopy, CancellationToken.None));
            Assert.Equal(Models.ErrorCode.INVESTMENT_ACCOUNT_NOT_FOUND, exception.ErrorCode);
        }

        [Fact]
        public async Task CreateTradeCommand_ShouldThrowSecurityNotFound_WhenSecurityIsInvalid()
        {
            InitTradeTest(out var createTradeCommandHandler, out var mapper, out var userIdToTest, out var tradeEntities, out var userEntities, out var securityEntities);

            var firstTrade = tradeEntities.First();
            var firstTradeUser = userEntities.First(x => x.Id == firstTrade.UserId);

            var createTradeCopy = mapper.Map<CreateTradeCommand>(firstTrade);
            //Assign an invalid security id
            createTradeCopy.SecurityId = securityEntities.Select(x => x.Id).Max() + 1;

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await createTradeCommandHandler.Handle(createTradeCopy, CancellationToken.None));
            Assert.Equal(Models.ErrorCode.SECURITY_NOT_FOUND, exception.ErrorCode);
        }

        private void InitTradeTest(out CreateTradeCommandHandler commandHandler, out IMapper mapper,out int userIdToTest, out List<TradeEntity> tradeEntities, out List<UserEntity> userEntities, out List<SecurityEntity> securityEntities)
        {
            userEntities = MockUserHelper.GetTestUserEntities();
            tradeEntities = MockTradeHelper.GetTestTradeEntities();

            userIdToTest = tradeEntities.First().UserId;
            mapper = MappingProfileTests.GetTestMapperConfigurationForAllProfiles().CreateMapper();

            var mockUserContextService = MockUserHelper.InitMockUserContextService(userIdToTest);
            var mockUserRepository = MockUserHelper.InitMockUserRepository(userEntities);

            var mockTradeRepository = MockTradeHelper.InitMockTradeRepositoryReadOnly(tradeEntities);
            mockTradeRepository
                .Setup(x => x.CreateTradeAsync(It.IsAny<TradeEntity>()))
                .ReturnsAsync(1);

            securityEntities = MockSecurityHelper.GetTestSecurityEntities();
            var mockSecurityRepository = MockSecurityHelper.InitMockSecurityRepository(securityEntities);

            var mockMessageBus = new Mock<IMessageBus>();
            mockMessageBus
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
