using KoggoInvestments.Domain.Stocks;

namespace KoggoInvestments.Application.RepositoryInterfaces;

public interface IStockDataRepository
{
    Task<List<StockInfo>?> GetStockDataAsync();
    Task SaveStockDataAsync(List<StockInfo> stockData);
}