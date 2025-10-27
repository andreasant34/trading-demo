using Trading.Core.Entities;

namespace Trading.Core.Interfaces.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> ListUsersAsync();

        Task<UserEntity?> GetUserByEmailAsync(string email);
    }
}
