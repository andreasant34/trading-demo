using AutoMapper;
using MediatR;
using Trading.Core.Entities;
using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.Core.Commands
{
    public class CreateTradeCommand : IRequest<int>
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
        private readonly IUserContextService _userContextService;
        private readonly ITradeRepository _tradeRepository;
        private readonly IMapper _mapper;

        public CreateTradeCommandHandler(IUserContextService userContext, ITradeRepository tradeRepository, IMapper mapper)
        {
            _userContextService = userContext;
            _tradeRepository = tradeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTradeCommand request, CancellationToken cancellationToken)
        {
            var tradeEntity = _mapper.Map<TradeEntity>(request);
            tradeEntity.UserId = _userContextService.GetUserId();
            var tradeId = await _tradeRepository.CreateTradeAsync(tradeEntity);
            return tradeId;
        }
    }
}
