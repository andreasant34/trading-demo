using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Infrastructure.Data.Models;

namespace Trading.Infrastructure.Data.Configurations
{
    internal class TradeConfiguration : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.UserId, x.InvestmentAccountId});
            builder.HasIndex(x => x.SecurityId);
            
            builder.HasOne(x => x.Security)
                .WithMany()//Untracked to ensure we do not accidentally query all trades linked with a security (across all users)
                .HasForeignKey(trade => trade.SecurityId);

            builder.HasOne(x => x.User)
                .WithMany(user => user.Trades)
                .HasForeignKey(trade => trade.UserId);

            builder.HasOne(x => x.InvestmentAccount)
                .WithMany(account => account.Trades)
                .HasForeignKey(trade => new { trade.UserId, trade.InvestmentAccountId });
        }
    }
}
