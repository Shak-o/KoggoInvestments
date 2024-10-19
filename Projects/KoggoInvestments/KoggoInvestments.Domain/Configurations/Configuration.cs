namespace KoggoInvestments.Domain.Configurations;

public class Configuration
{
    public required string StockIdentifier { get; set; }
    public decimal MaxStockPrice { get; set; }
    public decimal MinStockPrice { get; set; }
}