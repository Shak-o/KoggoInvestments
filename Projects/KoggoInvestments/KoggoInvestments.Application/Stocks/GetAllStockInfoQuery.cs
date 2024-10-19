using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class GetAllStockInfoQuery : IRequest<List<StockInfo>?>
{
    
}