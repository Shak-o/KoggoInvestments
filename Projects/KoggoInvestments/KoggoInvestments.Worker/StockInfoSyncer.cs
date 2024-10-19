using KoggoInvestments.Application.Stocks;
using MediatR;

namespace KoggoInvestments.Worker;

public class StockInfoSyncer(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            
            var query = new SetStockInfoCommand();
            var result = await mediator.Send(query, cancellationToken: stoppingToken);
            
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}