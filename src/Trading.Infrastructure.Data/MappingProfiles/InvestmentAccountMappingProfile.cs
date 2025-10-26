using AutoMapper;
using Trading.Core.Models;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.MappingProfiles
{
    internal class InvestmentAccountMappingProfile : Profile
    {
        public InvestmentAccountMappingProfile()
        {
            _ = CreateMap<InvestmentAccount, InvestmentAccountDetails>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.UserId, target => target.MapFrom(source => source.UserId))
                .ForMember(target => target.Name, target => target.MapFrom(source => source.Name))
                .ReverseMap();
        }
    }
}
