namespace KoggoInvestments.Domain.Stocks;

public class StockDetailViewModel
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string DisplaySymbol { get; set; }
    public required string Symbol { get; set; }
    public required string Type { get; set; }
    public decimal Price { get; set; }
    public decimal Diff { get; set; }
}