using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using KoggoInvestments.Persistence.Models;
using MongoDB.Driver;

namespace KoggoInvestments.Persistence.Repositories;

public class StockDataRepository(IMongoClient mongoClient) : IStockDataRepository
{
    private readonly IMongoDatabase _db = mongoClient.GetDatabase("KoggoDb");

    public async Task<List<StockDetailViewModel>> GetStockDataAsync()
    {
        var collection = _db.GetCollection<StockDetailViewModel>("StockDetails");
        var result = await collection.Find(s => true).ToListAsync();
        return result;
    }

    public async Task<bool> CheckIfEmptyAsync()
    {
        var collection = _db.GetCollection<StockDetailViewModel>("StockDetails");
        var filter = Builders<StockDetailViewModel>.Filter.Empty; // This means no filtering, we want all documents
        var count = await collection.CountDocumentsAsync(filter);
        return count == 0;
    }

    public async Task SaveStockDataAsync(StockDetailViewModel stockDetails)
    {
        var collection = _db.GetCollection<StockDetailViewModel>("StockDetails");

        await collection.InsertOneAsync(stockDetails);
    }

    public async Task SavePolygonStockDataAsync(List<StockBarInfo> stocks, string stockIdentifier)
    {
        var collection = _db.GetCollection<StockBarInfo>(stockIdentifier);

        await collection.InsertManyAsync(stocks);
    }

    public async Task<StockBarInfo> GetStockBarInfoAsync(string stockIdentifier)
    {
        var collection = _db.GetCollection<StockBarInfo>(stockIdentifier);

        int lastId;
        var accessorCollection = _db.GetCollection<AccessModel>("LastAccessedStamp");
        var lastSavedId = await accessorCollection
            .Find(x => x.StockIdentifier == stockIdentifier)
            .FirstOrDefaultAsync();
        if (lastSavedId == null)
        {
            var sort = Builders<StockBarInfo>.Sort.Ascending(x => x.Timestamp);
            var firstBarInfo = await collection.Find(s => true)
                .Sort(sort)
                .Limit(1)
                .FirstOrDefaultAsync();

            lastId = firstBarInfo.Id;

            var maxId = await GetMaxAccessIdAsync();
            maxId++;
            await accessorCollection.InsertOneAsync(new AccessModel()
                { Id = ++maxId, StockIdentifier = stockIdentifier, LastAccessedId = lastId });

            return firstBarInfo;
        }

        lastId = ++lastSavedId.LastAccessedId;
        
        var barInfo = await collection.Find(x => x.Id == lastId).FirstAsync();
        var filter = Builders<AccessModel>.Filter.Eq(x => x.StockIdentifier, stockIdentifier);

// Define the update to set the new value for lastSavedStamp
        var update = Builders<AccessModel>.Update.Set(x => x.LastAccessedId, lastId);

// Perform the update operation
        await accessorCollection.UpdateOneAsync(filter, update);
        
        return barInfo;
    }
    
    private async Task<int> GetMaxAccessIdAsync()
    {
        var sort = Builders<AccessModel>.Sort.Descending(x => x.Id);
        var collectionName = $"LastAccessedStamp";
        var collection = _db.GetCollection<AccessModel>(collectionName);
        var result = await collection.Find(s => true)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();

        return result?.Id ?? 0;
    }
}