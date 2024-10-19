using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MongoDB.Driver;

namespace KoggoInvestments.Persistence.Repositories;

public class StockDataRepository(IMongoClient mongoClient) : IStockDataRepository
{
    public async Task<List<StockInfo>?> GetStockDataAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        var limit = pageSize;

        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockInfo>("StockData");
        var result = await collection.Find(FilterDefinition<StockInfo>.Empty)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
        
        return result;
    }

    public async Task SaveStockDataAsync(StockDetails stockDetails)
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockDetails>("StockData");
        
        await collection.InsertOneAsync(stockDetails);
    }
}