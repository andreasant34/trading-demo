using Microsoft.EntityFrameworkCore;
using Trading.Core.Entities;
using Trading.Core.Interfaces.Data;

namespace Trading.Infrastructure.Data.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly TradingDbContext _dbContext;

        public TradeRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateTradeAsync(TradeEntity trade)
        {
            _ = _dbContext.Trades.Add(trade);
            _ = await _dbContext.SaveChangesAsync();
            return trade.Id;
        }

        public async Task<IEnumerable<TradeEntity>> ListTradesByUserAsync(int userId)
        {
            return await _dbContext.Trades
                .Include(x => x.Security)
                .Include(x => x.InvestmentAccount)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TradeEntity>> ListTradesByUserSecurityAsync(int userId, int securityId)
        {
            return await _dbContext.Trades
                .Include(x => x.Security)
                .Include(x => x.InvestmentAccount)
                .Where(x => x.UserId == userId && x.SecurityId == securityId)
                .ToListAsync();
        }
    }
}
