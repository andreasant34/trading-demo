using Trading.Core.Models;

namespace Trading.Core.Tests.Comparers
{
    public class SecurityDetailsComparer : IEqualityComparer<SecurityDetails>
    {
        public bool Equals(SecurityDetails? x, SecurityDetails? y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(SecurityDetails obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}
