using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class InvestmentAccountConfiguration : IEntityTypeConfiguration<InvestmentAccount>
    {
        public void Configure(EntityTypeBuilder<InvestmentAccount> builder)
        {
            _ = builder.Property(x => x.Id).ValueGeneratedOnAdd();
            _ = builder.HasKey(x => new { x.UserId, x.Id });

            _ = builder.HasOne(x => x.User)
                .WithMany(user => user.InvestmentAccounts)
                .HasForeignKey(account => account.UserId);
        }
    }
}