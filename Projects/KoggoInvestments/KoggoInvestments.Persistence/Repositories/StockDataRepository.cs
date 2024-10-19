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

    public async Task<bool> CheckIfEmptyAsync()
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockDetailViewModel>("StockDetails");
        var filter = Builders<StockDetailViewModel>.Filter.Empty; // This means no filtering, we want all documents
        var count = await collection.CountDocumentsAsync(filter);
        return count == 0;
    }

    public async Task SaveStockDataAsync(StockDetailViewModel stockDetails)
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockDetailViewModel>("StockDetails");
        
        await collection.InsertOneAsync(stockDetails);
    }

    public async Task SavePolygonStockDataAsync(List<StockBarInfo> stocks, string stockIdentifier)
    {
        var db = mongoClient.GetDatabase("KoggoDb");
        var collection = db.GetCollection<StockBarInfo>(stockIdentifier);
        
        await collection.InsertManyAsync(stocks);
    }

    public Task GetStockBarInfoAsync(string stockIdentifier, int quantity, int page)
    {
        throw new NotImplementedException();
    }
}