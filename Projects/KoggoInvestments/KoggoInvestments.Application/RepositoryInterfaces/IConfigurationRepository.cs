using KoggoInvestments.Domain.Configurations;

namespace KoggoInvestments.Application.RepositoryInterfaces;

public interface IConfigurationRepository
{
    Task AddConfigurationAsync(NotificationConfiguration notificationConfiguration);
    Task<int> GetMaxIdAsync(string stockIdentifier);
    Task<List<NotificationConfiguration>> GetAllConfigurationsAsync();
}