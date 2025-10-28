using AutoMapper;
using Trading.Core.Models;
using Trading.Core.Entities;

namespace Trading.Core.MappingProfiles
{
    public class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            _ = CreateMap<SecurityEntity, SecurityDetails>()
                .ReverseMap();
        }
    }
}
