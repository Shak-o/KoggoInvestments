using KoggoInvestments.Domain.Stocks;

namespace KoggoInvestments.Application.RepositoryInterfaces;

public interface IStockDataRepository
{
    Task<List<StockDetailViewModel>> GetStockDataAsync();
    Task SaveStockDataAsync(StockDetailViewModel stockData);
}