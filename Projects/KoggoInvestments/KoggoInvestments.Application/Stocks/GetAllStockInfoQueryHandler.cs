using KoggoInvestments.Application.ApiInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class GetAllStockInfoQueryHandler(IFinnHubApi _finnHubApi) : IRequestHandler<GetAllStockInfoQuery, List<StockInfo>>
{
    public async Task<List<StockInfo>> Handle(GetAllStockInfoQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}