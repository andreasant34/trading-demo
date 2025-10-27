using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Core.Entities;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasIndex(x => x.Email);
        }
    }
}
