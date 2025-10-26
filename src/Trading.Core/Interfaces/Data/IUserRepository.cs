using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;

namespace Trading.Core.Interfaces.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDetails>> ListUsersAsync();

        Task<UserDetails?> GetUserByEmailAsync(string email);
    }
}
