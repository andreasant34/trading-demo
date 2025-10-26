using AutoMapper;
using MediatR;
using Trading.Core.Interfaces;

namespace Trading.API.Queries
{
    public class ListTradesQuery:IRequest<IEnumerable<ListTradesDto>>
    {
        public int UserId { get; set; }
    }

    public class ListTradesQueryHandler : IRequestHandler<ListTradesQuery, IEnumerable<ListTradesDto>>
    {
        private readonly ITradingService _tradingService;
        private readonly IMapper _mapper;

        public ListTradesQueryHandler(ITradingService tradingService, IMapper mapper)
        {
            _tradingService = tradingService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListTradesDto>> Handle(ListTradesQuery request, CancellationToken cancellationToken)
        {
            var trades = await _tradingService.ListTradesByUserAsync(request.UserId);
            return _mapper.Map<IEnumerable<ListTradesDto>>(trades);
        }
    }
}
