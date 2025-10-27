using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Core.Entities;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class SecurityConfiguration : IEntityTypeConfiguration<SecurityEntity>
    {
        public void Configure(EntityTypeBuilder<SecurityEntity> builder)
        {
            _ = builder.HasKey(x => x.Id);
        }
    }
}
