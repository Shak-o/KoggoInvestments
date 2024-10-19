using KoggoInvestments.Domain.Stocks;
using RestEase;

namespace KoggoInvestments.Application.ApiInterfaces;

public interface IFinnHubApi
{
    [Query("token")]
    public string ApiKey { get; set; }
    
    [Get("api/v1/search?exchange=US")]
    Task<StockInfo> GetStockInfoAsync([Query] string token, [Query] string q);
}