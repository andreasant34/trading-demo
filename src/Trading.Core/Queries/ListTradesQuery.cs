using AutoMapper;
using MediatR;
using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.Core.Queries
{
    public class ListTradesQuery : IRequest<IEnumerable<TradeDetails>>
    {
    }

    public class ListTradesQueryHandler : IRequestHandler<ListTradesQuery, IEnumerable<TradeDetails>>
    {
        private readonly IUserContextService _userContextService;
        private readonly ITradeRepository _tradeRepository;
        private readonly IMapper _mapper;

        public ListTradesQueryHandler(IUserContextService userContextService, ITradeRepository tradeRepository, IMapper mapper)
        {
            _userContextService = userContextService;
            _tradeRepository = tradeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TradeDetails>> Handle(ListTradesQuery request, CancellationToken cancellationToken)
        {
            var dbTrades = await _tradeRepository.ListTradesByUserAsync(_userContextService.GetUserId());
            return _mapper.Map<IEnumerable<TradeDetails>>(dbTrades);
        }
    }
}
