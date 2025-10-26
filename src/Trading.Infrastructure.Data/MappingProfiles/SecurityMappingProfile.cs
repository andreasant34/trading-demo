using AutoMapper;
using Trading.Core.Models;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.MappingProfiles
{
    internal class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            _ = CreateMap<Security, SecurityDetails>()
                .ReverseMap();
        }
    }
}
