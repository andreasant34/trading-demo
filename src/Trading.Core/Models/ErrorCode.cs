namespace Trading.Core.Models
{
    public enum ErrorCode
    {
        UNKNOWN = 0,
        VALIDATION = 1,
        TRADE_SELL_QUANTITY_NOT_AVAILABLE = 2,
        INVESTMENT_ACCOUNT_NOT_FOUND = 3,
        SECURITY_NOT_FOUND = 4
    }
}
