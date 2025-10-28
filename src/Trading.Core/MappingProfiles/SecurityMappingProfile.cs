using AutoMapper;
using Trading.Core.Entities;
using Trading.Core.Models;

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
