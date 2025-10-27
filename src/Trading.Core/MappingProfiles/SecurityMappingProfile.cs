using AutoMapper;
using Trading.Core.Models;
using Trading.Core.Entities;

namespace Trading.Core.MappingProfiles
{
    internal class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            _ = CreateMap<SecurityEntity, SecurityDetails>()
                .ReverseMap();
        }
    }
}
