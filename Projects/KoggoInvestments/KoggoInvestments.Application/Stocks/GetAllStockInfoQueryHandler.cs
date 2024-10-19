using KoggoInvestments.Application.ApiInterfaces;
using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class GetAllStockInfoQueryHandler(IFinnHubApi finnHubApi, IStockDataRepository stockDataRepository) : IRequestHandler<GetAllStockInfoQuery, List<StockInfo>?>
{
    public async Task<List<StockInfo>?> Handle(GetAllStockInfoQuery request, CancellationToken cancellationToken)
    {
        var repoResult = await stockDataRepository.GetStockDataAsync();

        if (repoResult == null || !repoResult.Any())
        {
            var finnResult = await finnHubApi.GetStockInfoAsync(token: finnHubApi.ApiKey);
            await stockDataRepository.SaveStockDataAsync(finnResult);
            return finnResult;
        }
        
        return repoResult;
    }
}