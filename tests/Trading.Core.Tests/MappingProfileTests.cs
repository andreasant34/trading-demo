using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Trading.Core.MappingProfiles;

namespace Trading.Core.Tests
{
    public class MappingProfileTests
    {
        [Fact]
        public void UserInvestmentMappingProfile_ShouldBeValid()
        {
            AssertMappingProfilesAreValid<UserMappingProfile, InvestmentAccountMappingProfile>();
        }

        [Fact]
        public void SecurityMappingProfile_ShouldBeValid()
        {
            AssertMappingProfileIsValid<SecurityMappingProfile>();
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

        private void AssertMappingProfilesAreValid<TProfile1, TProfile2>() 
            where TProfile1 : Profile, new()
            where TProfile2 : Profile, new()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile1>();
                cfg.AddProfile<TProfile2>();
            }, new NullLoggerFactory());

            configuration.AssertConfigurationIsValid();
        }
    }
}
