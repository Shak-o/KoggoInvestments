using KoggoInvestments.Domain.Stocks;

namespace KoggoInvestments.Application.RepositoryInterfaces;

public interface IStockDataRepository
{
    Task<List<StockInfo>?> GetStockDataAsync(int pageNumber, int pageSize);
    Task SaveStockDataAsync(StockDetails stockData);
}