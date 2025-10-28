using Moq;
using Trading.Core.Entities;
using Trading.Core.Interfaces.Data;

namespace Trading.Core.Tests.MockHelpers
{
    public static class MockSecurityHelper
    {
        public static Mock<ISecurityRepository> InitMockSecurityRepository(List<SecurityEntity> securityEntities)
        {
            var mockSecurityRepository = new Mock<ISecurityRepository>();

            _ = mockSecurityRepository
                .Setup(x => x.ListSecuritiesAsync())
                .ReturnsAsync(securityEntities);

            _ = mockSecurityRepository
                .Setup(x => x.GetSecurityByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int securityId) => securityEntities.FirstOrDefault(x => x.Id == securityId));

            return mockSecurityRepository;
        }

        public static List<SecurityEntity> GetTestSecurityEntities()
        {
            var result = new List<SecurityEntity>();
            for (var i = 1; i <= 5; i++)
            {
                result.Add(new SecurityEntity
                {
                    Id = i,
                    Name = $"Test Security {i}"
                });
            }
            return result;
        }
    }
}
