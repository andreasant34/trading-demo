using AutoMapper;
using MediatR;
using Trading.Core.Interfaces;
using Trading.Core.Models;

namespace Trading.API.Commands
{
    public class CreateTradeCommand:IRequest<int>
    {
        public int InvestmentAccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int SecurityId { get; set; }
        public int Quantity { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class CreateTradeCommandHandler : IRequestHandler<CreateTradeCommand, int>
    {
        private readonly ITradingService _tradingService;
        private readonly IMapper _mapper;

        public CreateTradeCommandHandler(ITradingService tradingService, IMapper mapper)
        {
            _tradingService = tradingService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTradeCommand request, CancellationToken cancellationToken)
        {
            var trade = _mapper.Map<TradeCreationDetails>(request);
            trade.UserId = 1;//TODO

            var tradeId = await _tradingService.CreateTradeAsync(trade);
            return tradeId;
        }
    }
}
