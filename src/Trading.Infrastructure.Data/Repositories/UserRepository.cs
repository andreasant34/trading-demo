using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.Infrastructure.Data.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly TradingDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(TradingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDetails?> GetUserByEmailAsync(string email)
        {
            var dbUser = await _dbContext.Users.Include(x => x.InvestmentAccounts).FirstOrDefaultAsync(x => x.Email == email);
            return dbUser == null ? null : _mapper.Map<UserDetails>(dbUser);
        }

        public async Task<IEnumerable<UserDetails>> ListUsersAsync()
        {
            var dbUsers = await _dbContext.Users.Include(x => x.InvestmentAccounts).ToListAsync();
            return _mapper.Map<IEnumerable<UserDetails>>(dbUsers);
        }
    }
}
