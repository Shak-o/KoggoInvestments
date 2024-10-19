using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Configurations;
using MongoDB.Driver;

namespace KoggoInvestments.Persistence.Repositories;

public class ConfigurationRepository(IMongoClient mongoClient) : IConfigurationRepository
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("KoggoDb");
    public async Task AddConfigurationAsync(NotificationConfiguration notificationConfiguration)
    {
        var collectionName = $"{nameof(NotificationConfiguration)}";
        var collection = _database.GetCollection<NotificationConfiguration>(collectionName);
        
        await collection.InsertOneAsync(notificationConfiguration);
    }

    public async Task<int> GetMaxIdAsync(string stockIdentifier)
    {
        var sort = Builders<NotificationConfiguration>.Sort.Descending(x => x.Id);
        var collectionName = $"{nameof(NotificationConfiguration)}";
        var collection = _database.GetCollection<NotificationConfiguration>(collectionName);
        var result = await collection.Find(s => true)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();

        return result?.Id ?? 0;
    }

    public async Task<List<NotificationConfiguration>> GetAllConfigurationsAsync()
    {
        var collection = _database.GetCollection<NotificationConfiguration>(nameof(NotificationConfiguration));
        
        var result = await collection.Find(_ => true).ToListAsync();
        return result;
    }
}