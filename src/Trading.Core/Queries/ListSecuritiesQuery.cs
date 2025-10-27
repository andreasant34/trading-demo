using AutoMapper;
using MediatR;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.Core.Queries
{
    public class ListSecuritiesQuery : IRequest<IEnumerable<SecurityDetails>>
    {
    }

    public class ListSecuritiesQueryHandler : IRequestHandler<ListSecuritiesQuery, IEnumerable<SecurityDetails>>
    {
        private readonly ISecurityRepository _securityRepository;
        private readonly IMapper _mapper;

        public ListSecuritiesQueryHandler(ISecurityRepository securityRepository, IMapper mapper)
        {
            _securityRepository = securityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SecurityDetails>> Handle(ListSecuritiesQuery request, CancellationToken cancellationToken)
        {
            var dbSecurities = await _securityRepository.ListSecuritiesAsync();
            return _mapper.Map<IEnumerable<SecurityDetails>>(dbSecurities);
        }
    }
}