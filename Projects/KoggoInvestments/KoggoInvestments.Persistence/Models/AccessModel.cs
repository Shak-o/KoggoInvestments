namespace KoggoInvestments.Persistence.Models;

public class AccessModel
{
    public int Id { get; set; }
    public required string StockIdentifier { get; set; }
    public int LastAccessedId { get; set; }
}