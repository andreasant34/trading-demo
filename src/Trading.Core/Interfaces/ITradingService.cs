using Trading.Core.Models;

namespace Trading.Core.Interfaces
{
    public interface ITradingService
    {
        Task<IEnumerable<TradeDetails>> ListTradesByUserAsync(int userId);

        Task<int> CreateTradeAsync(TradeCreationDetails trade);
    }
}
