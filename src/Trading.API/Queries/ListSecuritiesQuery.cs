using AutoMapper;
using MediatR;
using Trading.Core.Interfaces.Data;

namespace Trading.API.Queries
{
    public class ListSecuritiesQuery : IRequest<IEnumerable<ListSecuritiesDto>>
    {
    }

    public class ListSecuritiesQueryHandler : IRequestHandler<ListSecuritiesQuery, IEnumerable<ListSecuritiesDto>>
    {
        private readonly ISecurityRepository _securityRepository;
        private readonly IMapper _mapper;

        public ListSecuritiesQueryHandler(ISecurityRepository securityRepository, IMapper mapper)
        {
            _securityRepository = securityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListSecuritiesDto>> Handle(ListSecuritiesQuery request, CancellationToken cancellationToken)
        {
            var securities = await _securityRepository.ListSecuritiesAsync();
            return _mapper.Map<IEnumerable<ListSecuritiesDto>>(securities);
        }
    }
}