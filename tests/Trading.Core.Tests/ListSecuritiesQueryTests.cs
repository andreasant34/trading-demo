using Moq;
using Trading.Core.Entities;
using Trading.Core.Interfaces.Data;
using Trading.Core.MappingProfiles;
using Trading.Core.Models;
using Trading.Core.Queries;
using Trading.Core.Tests.MockHelpers;

namespace Trading.Core.Tests
{
    public class ListSecuritiesQueryTests
    {
        [Fact]
        public async Task ListSecuritiesQuery_ShouldReturnAllSecurityDetails()
        {
            var securityEntities = MockSecurityHelper.GetTestSecurityEntities();

            var mapper = MappingProfileTests.GetTestMapperConfigurationForProfile<SecurityMappingProfile>().CreateMapper();
            var expectedResult = mapper.Map<IEnumerable<SecurityDetails>>(securityEntities).ToList();

            var mockSecurityRepository = new Mock<ISecurityRepository>();
            mockSecurityRepository.Setup(x => x.ListSecuritiesAsync()).ReturnsAsync(securityEntities);

            var queryHandler = new ListSecuritiesQueryHandler(mockSecurityRepository.Object, mapper);

            var result = await queryHandler.Handle(new ListSecuritiesQuery(),CancellationToken.None);

            Assert.Equal(expectedResult, result);
        }
    }
}
