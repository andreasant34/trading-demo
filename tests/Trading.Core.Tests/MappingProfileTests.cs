using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Trading.Core.MappingProfiles;

namespace Trading.Core.Tests
{
    public class MappingProfileTests
    {
        [Fact]
        public void UserMappingProfile_ShouldBeValid()
        {
            AssertMappingProfileIsValid<UserMappingProfile>();
        }

        [Fact]
        public void SecurityMappingProfile_ShouldBeValid()
        {
            AssertMappingProfileIsValid<SecurityMappingProfile>();
        }

        [Fact]
        public void InvestmentAccountMappingProfile_ShouldBeValid()
        {
            AssertMappingProfileIsValid<InvestmentAccountMappingProfile>();
        }

        [Fact]
        public void TradeMappingProfile_ShouldBeValid()
        {
            AssertMappingProfileIsValid<TradeMappingProfile>();
        }

        private void AssertMappingProfileIsValid<TProfile>() where TProfile:Profile,new()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile>();
            }, new NullLoggerFactory());

            configuration.AssertConfigurationIsValid();
        }
    }
}
