using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Models
{
    public class InvestmentAccountDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }
    }
}
