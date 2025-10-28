using Trading.Core.Models;

namespace Trading.Core.Entities
{
    public class TradeEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public int InvestmentAccountId { get; set; }
        public InvestmentAccountEntity? InvestmentAccount { get; set; }
        public TransactionType TransactionType { get; set; }
        public int SecurityId { get; set; }
        public SecurityEntity? Security { get; set; }
        public int Quantity { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
