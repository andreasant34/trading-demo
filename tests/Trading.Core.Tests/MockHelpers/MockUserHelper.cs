using Moq;
using Trading.Core.Entities;
using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;

namespace Trading.Core.Tests.MockHelpers
{
    public static class MockUserHelper
    {
        public static Mock<IUserContextService> InitMockUserContextService(int userIdToTest)
        {
            var mockUserContextService = new Mock<IUserContextService>();
            _ = mockUserContextService.Setup(x => x.GetUserId()).Returns(userIdToTest);
            return mockUserContextService;
        }

        public static Mock<IUserRepository> InitMockUserRepository(List<UserEntity> userEntities)
        {
            var mockUserRepository = new Mock<IUserRepository>();

            _ = mockUserRepository
                .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int userId) => userEntities.FirstOrDefault(x => x.Id == userId));

            _ = mockUserRepository
                .Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => userEntities.FirstOrDefault(x => x.Email == email));

            return mockUserRepository;
        }

        public static List<UserEntity> GetTestUserEntities()
        {
            var result = new List<UserEntity>();

            for (var i = 1; i < 3; i++)
            {
                result.Add(new UserEntity
                {
                    Id = i,
                    Email = $"email{i}@gmail.com",
                    Surname = $"Surname {i}",
                    Name = $"Name {i}",
                    InvestmentAccounts =
                    [
                        new InvestmentAccountEntity
                        {
                            Id = i,//Let the investment account id and the user id match
                            UserId = i,
                            Name = $"Investment account {i}"
                        }
                    ]
                });
            }

            return result;
        }
    }
}
