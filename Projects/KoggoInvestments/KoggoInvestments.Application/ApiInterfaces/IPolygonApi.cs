using KoggoInvestments.Domain.Stocks;
using RestEase;

namespace KoggoInvestments.Application.ApiInterfaces;

public interface IPolygonApi
{
    [Get("/v2/aggs/ticker/{stocksTicker}/range/1/minute/2023-01-09/2023-02-10?adjusted=true&sort=asc")]
    Task<Stock> GetStockAsync([Path] string stocksTicker, [Query] string apiKey); 
}