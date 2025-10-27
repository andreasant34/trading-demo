namespace Trading.Core.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required List<InvestmentAccountEntity> InvestmentAccounts { get; set; }
        public required List<TradeEntity> Trades { get; set; }
    }
}
