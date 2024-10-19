﻿using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
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

    public Task GetStockBarInfoAsync(string stockIdentifier, int quantity, int page)
    {
        throw new NotImplementedException();
    }
}