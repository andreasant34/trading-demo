using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required List<InvestmentAccountDetails> InvestmentAccounts { get; set; }
    }
}