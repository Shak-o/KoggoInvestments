using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MongoDB.Driver;

namespace KoggoInvestments.Persistence.Repositories;

public class StockDataRepository(IMongoClient mongoClient) : IStockDataRepository
{
    public async Task<List<StockDetailViewModel>> GetStockDataAsync()
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockDetailViewModel>("StockDetails");
        var result = await collection.Find(s => true).ToListAsync();
        return result;
    }

    public async Task SaveStockDataAsync(StockDetailViewModel stockDetails)
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockDetailViewModel>("StockDetails");
        
        await collection.InsertOneAsync(stockDetails);
    }

    public Task SavePolygonStockDataAsync(List<Stock> stocks, string stockIdentifier)
    {
        throw new NotImplementedException();
    }
}