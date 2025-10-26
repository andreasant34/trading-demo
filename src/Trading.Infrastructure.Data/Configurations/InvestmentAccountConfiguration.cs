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
    internal class InvestmentAccountConfiguration : IEntityTypeConfiguration<InvestmentAccount>
    {
        public void Configure(EntityTypeBuilder<InvestmentAccount> builder)
        {
            builder.HasKey(x => new { x.UserId, x.Id });

            builder.HasOne(x => x.User)
                .WithMany(user => user.InvestmentAccounts)
                .HasForeignKey(account => account.UserId);
        }
    }
}