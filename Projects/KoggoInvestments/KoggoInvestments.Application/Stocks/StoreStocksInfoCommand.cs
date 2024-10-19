using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class StoreStocksInfoCommand : IRequest<Unit>
{
    public required List<StockInfo> Stocks { get; set; }
}