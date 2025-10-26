using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class SecurityConfiguration : IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
            _ = builder.HasKey(x => x.Id);
        }
    }
}
