using Trading.Core.Entities;

namespace Trading.Core.Interfaces.Data
{
    public interface ISecurityRepository
    {
        Task<IEnumerable<SecurityEntity>> ListSecuritiesAsync();

        Task<SecurityEntity?> GetSecurityByIdAsync(int id);
    }
}
