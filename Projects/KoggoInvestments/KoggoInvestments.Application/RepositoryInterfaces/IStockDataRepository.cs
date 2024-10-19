using KoggoInvestments.Domain.Stocks;

namespace KoggoInvestments.Application.RepositoryInterfaces;

public interface IStockDataRepository
{
    Task<List<StockDetails>> GetStockDataAsync();
    Task SaveStockDataAsync(StockDetails stockData);
}