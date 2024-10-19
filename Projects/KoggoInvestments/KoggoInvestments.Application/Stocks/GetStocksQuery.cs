using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class GetStocksQuery : IRequest<List<StockDetails>>
{
    
}