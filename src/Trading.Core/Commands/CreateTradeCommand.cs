using AutoMapper;
using MediatR;
using Trading.Core.Entities;
using Trading.Core.Exceptions;
using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;
using Trading.Core.Interfaces.MessageBus;
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
        private readonly ISecurityRepository _securityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;

        public CreateTradeCommandHandler(
            IUserContextService userContext,
            ITradeRepository tradeRepository,
            ISecurityRepository securityRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IMessageBus messageBus)
        {
            _userContextService = userContext;
            _tradeRepository = tradeRepository;
            _securityRepository = securityRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _messageBus = messageBus;
        }

        public async Task<int> Handle(CreateTradeCommand request, CancellationToken cancellationToken)
        {
            var tradeEntity = _mapper.Map<TradeEntity>(request);
            tradeEntity.UserId = _userContextService.GetUserId();

            var userEntity = await _userRepository.GetUserByIdAsync(tradeEntity.UserId);
            var accountIds = userEntity!.InvestmentAccounts.Select(x => x.Id);
            if (!accountIds.Contains(tradeEntity.InvestmentAccountId))
            {
                throw new BadRequestException(ErrorCode.INVESTMENT_ACCOUNT_NOT_FOUND);
            }

            var securityEntity = await _securityRepository.GetSecurityByIdAsync(tradeEntity.SecurityId);
            if (securityEntity == null)
            {
                throw new BadRequestException(ErrorCode.SECURITY_NOT_FOUND);
            }

            if (tradeEntity.TransactionType == TransactionType.Sell)
            {
                var securityTrades = await _tradeRepository.ListTradesByUserSecurityAsync(tradeEntity.UserId, tradeEntity.SecurityId);
                var buyQuantity = securityTrades.Where(x => x.TransactionType == TransactionType.Buy).Sum(x => x.Quantity);
                var sellQuantity = securityTrades.Where(x => x.TransactionType == TransactionType.Sell).Sum(x => x.Quantity);
                var availableQuantity = buyQuantity - sellQuantity;

                if (tradeEntity.Quantity > availableQuantity)
                {
                    throw new BadRequestException(ErrorCode.TRADE_SELL_QUANTITY_NOT_AVAILABLE);
                }
            }

            var tradeId = await _tradeRepository.CreateTradeAsync(tradeEntity);
            tradeEntity.Id = tradeId;

            var tradeCreatedCommand = _mapper.Map<TradeCreatedCommand>(tradeEntity);
            await _messageBus.PublishAsync(tradeCreatedCommand);
            //await _messageBus.SendAsync(tradeCreatedCommand);

            return tradeId;
        }
    }
}
