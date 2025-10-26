using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.Repositories
{
    internal class TradeRepository : ITradeRepository
    {
        private readonly TradingDbContext _dbContext;
        private readonly IMapper _mapper;

        public TradeRepository(TradingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateTradeAsync(TradeCreationDetails trade)
        {
            var dbTrade = _mapper.Map<Trade>(trade);
            _ = _dbContext.Trades.Add(dbTrade);
            _ = await _dbContext.SaveChangesAsync();
            return dbTrade.Id;
        }

        public async Task<IEnumerable<TradeDetails>> ListTradesByUserAsync(int userId)
        {
            var dbTrades = await _dbContext.Trades
                .Include(x => x.Security)
                .Include(x => x.InvestmentAccount)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TradeDetails>>(dbTrades);
        }
    }
}
