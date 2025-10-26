namespace Trading.Infrastructure.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required List<InvestmentAccount> InvestmentAccounts { get; set; }
        public required List<Trade> Trades { get; set; }
    }
}
