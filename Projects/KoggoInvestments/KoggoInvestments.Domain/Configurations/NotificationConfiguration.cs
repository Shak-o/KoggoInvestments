namespace KoggoInvestments.Domain.Configurations;

public class NotificationConfiguration
{
    public int Id { get; set; }
    public required string StockIdentifier { get; set; }
    public decimal MaxStockPrice { get; set; }
    public decimal MinStockPrice { get; set; }
    public int MinPercent { get; set; }
    public int MaxPercent { get; set; }
}