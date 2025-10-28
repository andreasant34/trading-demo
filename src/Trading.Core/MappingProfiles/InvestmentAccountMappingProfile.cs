using AutoMapper;
using Trading.Core.Models;
using Trading.Core.Entities;

namespace Trading.Core.MappingProfiles
{
    public class InvestmentAccountMappingProfile : Profile
    {
        public InvestmentAccountMappingProfile()
        {
            _ = CreateMap<InvestmentAccountEntity, InvestmentAccountDetails>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.UserId, target => target.MapFrom(source => source.UserId))
                .ForMember(target => target.Name, target => target.MapFrom(source => source.Name))
                .ReverseMap();
        }
    }
}
