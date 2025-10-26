using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;
using Trading.Infrastructure.Data.Models;

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
            if (dbSecurity == null)
                return null;

            return _mapper.Map<SecurityDetails>(dbSecurity);
        }

        public async Task<IEnumerable<SecurityDetails>> ListSecuritiesAsync()
        {
            var dbSecurities = await _dbContext.Securities.ToListAsync();
            return _mapper.Map<IEnumerable<SecurityDetails>>(dbSecurities);
        }
    }
}
