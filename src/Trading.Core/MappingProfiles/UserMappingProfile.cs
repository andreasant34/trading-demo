using AutoMapper;
using Trading.Core.Entities;
using Trading.Core.Models;

namespace Trading.Core.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            _ = CreateMap<UserEntity, UserDetails>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.Name, target => target.MapFrom(source => source.Name))
                .ForMember(target => target.Surname, target => target.MapFrom(source => source.Surname))
                .ForMember(target => target.Email, target => target.MapFrom(source => source.Email))
                .ForMember(target => target.InvestmentAccounts, target => target.MapFrom(source => source.InvestmentAccounts))
                .ReverseMap();
        }
    }
}
