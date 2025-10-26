using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.MappingProfiles
{
    internal class UserMappingProfile:Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<User, UserDetails>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.Name, target => target.MapFrom(source => source.Name))
                .ForMember(target => target.Surname, target => target.MapFrom(source => source.Surname))
                .ForMember(target => target.Email, target => target.MapFrom(source => source.Email))
                .ForMember(target => target.InvestmentAccounts, target => target.MapFrom(source => source.InvestmentAccounts))
                .ReverseMap();
        }
    }
}
