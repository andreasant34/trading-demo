using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trading.Core.Entities;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class TradeConfiguration : IEntityTypeConfiguration<TradeEntity>
    {
        public void Configure(EntityTypeBuilder<TradeEntity> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasIndex(x => new { x.UserId, x.InvestmentAccountId });
            _ = builder.HasIndex(x => new { x.UserId, x.SecurityId });
            _ = builder.HasIndex(x => x.SecurityId);

            _ = builder.HasOne(x => x.Security)
                .WithMany()//Untracked to ensure we do not accidentally query all trades linked with a security (across all users)
                .HasForeignKey(trade => trade.SecurityId);

            _ = builder.HasOne(x => x.User)
                .WithMany(user => user.Trades)
                .HasForeignKey(trade => trade.UserId);

            _ = builder.HasOne(x => x.InvestmentAccount)
                .WithMany(account => account.Trades)
                .HasForeignKey(trade => new { trade.UserId, trade.InvestmentAccountId });
        }
    }
}
