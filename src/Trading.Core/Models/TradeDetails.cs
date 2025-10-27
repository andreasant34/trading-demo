namespace Trading.Core.Models
{
    public class TradeDetails
    {
        public int InvestmentAccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int SecurityId { get; set; }
        public int Quantity { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string SecurityName { get; set; }
        public required string InvestmentAccountName { get; set; }
    }
}
