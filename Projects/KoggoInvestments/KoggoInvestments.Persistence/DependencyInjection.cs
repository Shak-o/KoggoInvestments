using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KoggoInvestments.Persistence;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddMongoDBClient("KoggoDb");
        builder.Services.AddScoped<IStockDataRepository, StockDataRepository>();
        builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
        
        return builder;
    }
}