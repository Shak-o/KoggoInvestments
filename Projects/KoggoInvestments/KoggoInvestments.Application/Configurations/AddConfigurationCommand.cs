using MediatR;

namespace KoggoInvestments.Application.Configurations;

public class AddConfigurationCommand : IRequest<Unit>
{
    public required string StockIdentifier { get; set; }
    public decimal MaxStockPrice { get; set; }
    public decimal MinStockPrice { get; set; }
    public int MinPercent { get; set; }
    public int MaxPercent { get; set; }
}