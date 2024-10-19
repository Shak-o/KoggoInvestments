using KoggoInvestments.Domain.Stocks;

namespace KoggoInvestments.Application.RepositoryInterfaces;

public interface IStockDataRepository
{
    Task<List<StockDetailViewModel>> GetStockDataAsync();
    Task<bool> CheckIfEmptyAsync();
    Task SaveStockDataAsync(StockDetailViewModel stockData);
    
    Task SavePolygonStockDataAsync(List<StockBarInfo> stocks, string stockIdentifier);
    
    Task<StockBarInfo> GetStockBarInfoAsync(string stockIdentifier);
}