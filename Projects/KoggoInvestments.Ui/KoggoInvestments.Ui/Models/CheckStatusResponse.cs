namespace KoggoInvestments.Ui.Models;

public class CheckStatusResponse
{
    public required string StockIdentifier { get; set; }
    public required int PercentValue { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    None,
    WentUp,
    WentDown
}