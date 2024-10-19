using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Configurations;
using MongoDB.Driver;

namespace KoggoInvestments.Persistence.Repositories;

public class ConfigurationRepository(IMongoClient mongoClient) : IConfigurationRepository
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("KoggoDb");
    public async Task AddConfigurationAsync(NotificationConfiguration notificationConfiguration)
    {
        var collectionName = $"{nameof(NotificationConfiguration)}_{notificationConfiguration.StockIdentifier}";
        var collection = _database.GetCollection<NotificationConfiguration>(collectionName);
        
        await collection.InsertOneAsync(notificationConfiguration);
    }

    public async Task<int> GetMaxIdAsync(string stockIdentifier)
    {
        var sort = Builders<NotificationConfiguration>.Sort.Descending(x => x.Id);
        var collectionName = $"{nameof(NotificationConfiguration)}_{stockIdentifier}";
        var collection = _database.GetCollection<NotificationConfiguration>(collectionName);
        var result = await collection.Find(s => true)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();

        return result?.Id ?? 0;
    }
}