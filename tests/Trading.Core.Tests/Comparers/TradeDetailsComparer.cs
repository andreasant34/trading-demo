using Trading.Core.Models;

namespace Trading.Core.Tests.Comparers
{
    public class TradeDetailsComparer : IEqualityComparer<TradeDetails>
    {
        public bool Equals(TradeDetails? x, TradeDetails? y)
            => x.InvestmentAccountId == y.InvestmentAccountId &&
                x.TransactionType == y.TransactionType &&
                x.SecurityId == y.SecurityId &&
                x.Quantity == y.Quantity &&
                x.CurrencyCode == y.CurrencyCode &&
                x.Price == y.Price &&
                x.TotalAmount == y.TotalAmount &&
                x.Id == y.Id &&
                x.UserId == y.UserId &&
                x.SecurityName == y.SecurityName &&
                x.InvestmentAccountName == y.InvestmentAccountName;

        public int GetHashCode(TradeDetails obj)
        {
            var firstProperties = HashCode.Combine(
                obj.InvestmentAccountId
                , obj.TransactionType
                , obj.SecurityId
                , obj.Quantity
                , obj.CurrencyCode
                , obj.Price
                , obj.TotalAmount
                , obj.Id
            );

            return HashCode.Combine(
                firstProperties
                , obj.UserId
                , obj.SecurityName
                , obj.InvestmentAccountName
            );
        }
    }

}
