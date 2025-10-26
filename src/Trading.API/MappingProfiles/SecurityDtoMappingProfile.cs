using AutoMapper;
using Trading.API.Queries;
using Trading.Core.Models;

namespace Trading.API.MappingProfiles
{
    public class SecurityDtoMappingProfile:Profile
    {
        public SecurityDtoMappingProfile()
        {
            CreateMap<SecurityDetails, ListSecuritiesDto>();
        }
    }
}
