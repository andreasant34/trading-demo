namespace Trading.Core.Entities
{
    public class InvestmentAccountEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required UserEntity User { get; set; }
        public required string Name { get; set; }
        public required List<TradeEntity> Trades { get; set; }
    }
}
