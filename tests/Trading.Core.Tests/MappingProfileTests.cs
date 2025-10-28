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
            var configuration = GetTestMapperConfigurationForMultipleProfiles<UserMappingProfile, InvestmentAccountMappingProfile>();
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void SecurityMappingProfile_ShouldBeValid()
        {
            var configuration = GetTestMapperConfigurationForProfile<SecurityMappingProfile>();
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void TradeMappingProfile_ShouldBeValid()
        {
            var configuration = GetTestMapperConfigurationForProfile<TradeMappingProfile>();
            configuration.AssertConfigurationIsValid();
        }

        public static MapperConfiguration GetTestMapperConfigurationForProfile<TProfile>() 
            where TProfile : Profile, new()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile>();
            }, new NullLoggerFactory());
        }

        public static MapperConfiguration GetTestMapperConfigurationForMultipleProfiles<TProfile1, TProfile2>()
            where TProfile1 : Profile, new()
            where TProfile2 : Profile, new()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile1>();
                cfg.AddProfile<TProfile2>();
            }, new NullLoggerFactory());
        }

        public static MapperConfiguration GetTestMapperConfigurationForAllProfiles()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(TradeMappingProfile).Assembly);
            }, new NullLoggerFactory());
        }
    }
}
