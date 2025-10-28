using Microsoft.EntityFrameworkCore;
using Trading.Core.Entities;
using Trading.Core.Interfaces.Data;

namespace Trading.Infrastructure.Data.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly TradingDbContext _dbContext;

        public SecurityRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SecurityEntity?> GetSecurityByIdAsync(int id)
        {
            return await _dbContext.Securities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<SecurityEntity>> ListSecuritiesAsync()
        {
            return await _dbContext.Securities.ToListAsync();
        }
    }
}
