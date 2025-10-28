using AutoMapper;
using Trading.Core.Entities;
using Trading.Core.Models;

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
