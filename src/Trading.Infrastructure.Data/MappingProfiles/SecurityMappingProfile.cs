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
    internal class SecurityMappingProfile:Profile
    {
        public SecurityMappingProfile()
        {
            CreateMap<Security, SecurityDetails>()
                .ReverseMap();
        }
    }
}
