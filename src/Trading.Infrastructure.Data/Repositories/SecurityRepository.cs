using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.Infrastructure.Data.Repositories
{
    internal class SecurityRepository : ISecurityRepository
    {
        private readonly TradingDbContext _dbContext;
        private readonly IMapper _mapper;

        public SecurityRepository(TradingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SecurityDetails?> GetSecurityByIdAsync(int id)
        {
            var dbSecurity = await _dbContext.Securities.FirstOrDefaultAsync(x => x.Id == id);
            return dbSecurity == null ? null : _mapper.Map<SecurityDetails>(dbSecurity);
        }

        public async Task<IEnumerable<SecurityDetails>> ListSecuritiesAsync()
        {
            var dbSecurities = await _dbContext.Securities.ToListAsync();
            return _mapper.Map<IEnumerable<SecurityDetails>>(dbSecurities);
        }
    }
}
