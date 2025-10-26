using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.Core.Services
{
    internal class TradingService : ITradingService
    {
        private readonly ITradeRepository _tradeRepository;

        public TradingService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<int> CreateTradeAsync(TradeCreationDetails trade)
        {
            return await _tradeRepository.CreateTradeAsync(trade);
        }

        public async Task<IEnumerable<TradeDetails>> ListTradesByUserAsync(int userId)
        {
            return await _tradeRepository.ListTradesByUserAsync(userId);
        }
    }
}
