using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Core.Entities;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class InvestmentAccountConfiguration : IEntityTypeConfiguration<InvestmentAccountEntity>
    {
        public void Configure(EntityTypeBuilder<InvestmentAccountEntity> builder)
        {
            _ = builder.Property(x => x.Id).ValueGeneratedOnAdd();
            _ = builder.HasKey(x => new { x.UserId, x.Id });

            _ = builder.HasOne(x => x.User)
                .WithMany(user => user.InvestmentAccounts)
                .HasForeignKey(account => account.UserId);
        }
    }
}