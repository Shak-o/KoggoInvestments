using KoggoInvestments.Domain.Stocks;
using RestEase;

namespace KoggoInvestments.Application.ApiInterfaces;

public interface IFinnHubApi
{
    [Query("token")]
    public string ApiKey { get; set; }
    
    [Get("api/v1/stock/symbol?exchange=US")]
    Task<List<StockInfo>> GetStockInfoAsync([Query] string token);
}