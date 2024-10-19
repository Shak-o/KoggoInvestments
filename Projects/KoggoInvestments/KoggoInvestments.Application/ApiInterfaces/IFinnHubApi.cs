using KoggoInvestments.Domain.Stocks;
using RestEase;

namespace KoggoInvestments.Application.ApiInterfaces;

public interface IFinnHubApi
{
    [Get("/v1/stock/symbol")]
    Task<List<StockInfo>> GetStockInfoAsync();
}