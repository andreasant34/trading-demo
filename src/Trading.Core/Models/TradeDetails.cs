namespace Trading.Core.Models
{
    public abstract class TradeDetailsBase
    {
        public int UserId { get; set; }
        public int InvestmentAccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int SecurityId { get; set; }
        public int Quantity { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TradeCreationDetails : TradeDetailsBase
    {
    }

    public class TradeDetails : TradeDetailsBase
    {
        public int Id { get; set; }
        public required string SecurityName { get; set; }
        public required string InvestmentAccountName { get; set; }
    }
}
