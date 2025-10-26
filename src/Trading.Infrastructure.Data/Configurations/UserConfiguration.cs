using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasIndex(x => x.Email);
        }
    }
}
