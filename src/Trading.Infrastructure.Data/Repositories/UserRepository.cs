using Microsoft.EntityFrameworkCore;
using Trading.Core.Entities;
using Trading.Core.Interfaces.Data;

namespace Trading.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TradingDbContext _dbContext;

        public UserRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.Include(x => x.InvestmentAccounts).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.Include(x => x.InvestmentAccounts).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<UserEntity>> ListUsersAsync()
        {
            return await _dbContext.Users.Include(x => x.InvestmentAccounts).ToListAsync();
        }
    }
}
