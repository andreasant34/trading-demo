using Trading.Core.Interfaces;

namespace Trading.API.Services
{
    /// <summary>
    /// Implements <see cref="IUserContextService"/> to provide details about the currently logged in user
    /// </summary>
    public class UserContextService : IUserContextService
    {
        public int GetUserId()
        {
            return 1;//TODO
        }
    }
}
