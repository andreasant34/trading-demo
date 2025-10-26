using Trading.Core.Models;

namespace Trading.Core.Interfaces.Data
{
    public interface ITradeRepository
    {
        Task<IEnumerable<TradeDetails>> ListTradesByUserAsync(int userId);

        Task<int> CreateTradeAsync(TradeCreationDetails trade);
    }
}
