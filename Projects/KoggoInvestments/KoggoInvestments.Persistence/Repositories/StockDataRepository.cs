using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;

namespace KoggoInvestments.Persistence.Repositories;

public class StockDataRepository : IStockDataRepository
{
    public async Task<List<StockInfo>?> GetStockDataAsync()
    {
        return null;
    }

    public Task SaveStockDataAsync(List<StockInfo> stockData)
    {
        return Task.CompletedTask;
    }
}