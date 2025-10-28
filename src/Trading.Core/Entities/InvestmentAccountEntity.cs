namespace Trading.Core.Entities
{
    public class InvestmentAccountEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public required string Name { get; set; }
        public List<TradeEntity>? Trades { get; set; }
    }
}
