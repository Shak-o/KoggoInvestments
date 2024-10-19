namespace KoggoInvestments.Domain.Stocks;

public class CheckStatusResponse
{
    public required string StockIdentifier { get; set; }
    public int PercentValue { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    None,
    WentUp,
    WentDown
}