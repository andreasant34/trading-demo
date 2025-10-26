namespace Trading.Infrastructure.Data.Models
{
    public class InvestmentAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public required string Name { get; set; }
        public required List<Trade> Trades { get; set; }
    }
}
