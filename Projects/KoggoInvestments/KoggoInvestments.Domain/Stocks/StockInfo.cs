namespace KoggoInvestments.Domain.Stocks;

public record StockInfo
{
    public int Count { get; set; } 
    public List<StockDetails> Result { get; set; }
}

public record StockDetails
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string DisplaySymbol { get; set; }
    public required string Symbol { get; set; }
    public required string Type { get; set; }
}