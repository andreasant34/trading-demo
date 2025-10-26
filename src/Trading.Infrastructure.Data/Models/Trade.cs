using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;

namespace Trading.Infrastructure.Data.Models
{
    public class Trade
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int InvestmentAccountId {  get; set; }
        public required InvestmentAccount InvestmentAccount { get; set; }
        public TransactionType TransactionType { get; set; }
        public int SecurityId { get; set; }
        public required Security Security { get; set; }
        public int Quantity { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
