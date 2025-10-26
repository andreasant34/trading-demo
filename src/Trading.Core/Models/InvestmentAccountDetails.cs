namespace Trading.Core.Models
{
    public class InvestmentAccountDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }
    }
}
