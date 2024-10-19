using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MongoDB.Driver;

namespace KoggoInvestments.Persistence.Repositories;

public class StockDataRepository(IMongoClient mongoClient) : IStockDataRepository
{
    public async Task<List<StockDetails>> GetStockDataAsync()
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockDetails>("StockDetails");
        var result = await collection.Find(s => true).ToListAsync();
        return result;
    }

    public async Task SaveStockDataAsync(StockDetails stockDetails)
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockDetails>("StockDetails");
        
        await collection.InsertOneAsync(stockDetails);
    }
}