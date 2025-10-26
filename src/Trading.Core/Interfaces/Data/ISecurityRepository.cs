using Trading.Core.Models;

namespace Trading.Core.Interfaces.Data
{
    public interface ISecurityRepository
    {
        Task<IEnumerable<SecurityDetails>> ListSecuritiesAsync();

        Task<SecurityDetails?> GetSecurityByIdAsync(int id);
    }
}
