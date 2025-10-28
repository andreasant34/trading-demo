using Moq;
using Trading.Core.Entities;
using Trading.Core.Interfaces.Data;
using Trading.Core.MappingProfiles;
using Trading.Core.Models;
using Trading.Core.Queries;

namespace Trading.Core.Tests
{
    public class ListSecuritiesQueryTests
    {
        [Fact]
        public async Task ListSecuritiesQueryHandler_ShouldReturnAllSecurityDetails()
        {
            var securityEntities = new List<SecurityEntity>
            {
                new SecurityEntity
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            var expectedResult = new List<SecurityDetails>
            {
                new SecurityDetails
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            var mockSecurityRepository = new Mock<ISecurityRepository>();
            mockSecurityRepository.Setup(x => x.ListSecuritiesAsync()).ReturnsAsync(securityEntities);

            var mapper = MappingProfileTests.GetTestMapperConfigurationForProfile<SecurityMappingProfile>().CreateMapper();
            var queryHandler = new ListSecuritiesQueryHandler(mockSecurityRepository.Object, mapper);

            var result = await queryHandler.Handle(new ListSecuritiesQuery(),CancellationToken.None);

            Assert.Equal(expectedResult, result);
        }
    }
}
