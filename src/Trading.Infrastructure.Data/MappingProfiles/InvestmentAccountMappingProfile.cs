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
    internal class InvestmentAccountMappingProfile:Profile
    {
        public InvestmentAccountMappingProfile()
        {
            CreateMap<InvestmentAccount, InvestmentAccountDetails>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.UserId, target => target.MapFrom(source => source.UserId))
                .ForMember(target => target.Name, target => target.MapFrom(source => source.Name))
                .ReverseMap();
        }
    }
}
