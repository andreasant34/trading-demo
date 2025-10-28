using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Trading.Core.Models;

namespace Trading.Core.Commands
{
    public class TradeCreatedCommand : IRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InvestmentAccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int SecurityId { get; set; }
        public int Quantity { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TradeCreatedCommandHandler : IRequestHandler<TradeCreatedCommand>
    {
        private readonly ILogger<TradeCreatedCommandHandler> _logger;

        public TradeCreatedCommandHandler(ILogger<TradeCreatedCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TradeCreatedCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Trade created: {JsonConvert.SerializeObject(request)}");
            return Task.CompletedTask;
        }
    }
}
