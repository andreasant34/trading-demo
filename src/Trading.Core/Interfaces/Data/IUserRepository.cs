using Trading.Core.Models;

namespace Trading.Core.Interfaces.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDetails>> ListUsersAsync();

        Task<UserDetails?> GetUserByEmailAsync(string email);
    }
}
