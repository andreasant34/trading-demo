using Trading.Core.Entities;

namespace Trading.Core.Interfaces.Data
{
    public interface ITradeRepository
    {
        Task<IEnumerable<TradeEntity>> ListTradesByUserAsync(int userId);
        Task<IEnumerable<TradeEntity>> ListTradesByUserSecurityAsync(int userId, int securityId);
        Task<int> CreateTradeAsync(TradeEntity trade);
    }
}
