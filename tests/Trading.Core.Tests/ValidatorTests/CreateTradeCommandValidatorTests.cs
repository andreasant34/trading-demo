using Trading.Core.Commands;
using Trading.Core.Tests.CommandTests;
using Trading.Core.Validators;

namespace Trading.Core.Tests.ValidatorTests
{
    public class CreateTradeCommandValidatorTests
    {
        [Fact]
        public void CreateTradeCommandValidator_ShouldFail_WhenTotalDoesNotAddUp()
        { 
            CreateTradeCommandTests.InitTradeTest(out var createTradeCommandHandler, out var mapper, out var userIdToTest, out var tradeEntities, out var userEntities, out var securityEntities);
            
            var tradeEntity = tradeEntities.First();
            var createTradeCommand = mapper.Map<CreateTradeCommand>(tradeEntity);
            createTradeCommand.TotalAmount += 999;

            var validator = new CreateTradeCommandValidator();
            var result = validator.Validate(createTradeCommand);

            Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("TotalAmount"));
        }
    }
}
