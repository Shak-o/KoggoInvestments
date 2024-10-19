using KoggoInvestments.Application.ApiInterfaces;
using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace KoggoInvestments.Application.Stocks;

public class SetStockInfoCommandHandler(IFinnHubApi finnHubApi, IStockDataRepository stockDataRepository, IConfiguration configuration)
    : IRequestHandler<SetStockInfoCommand, Unit>
{
    public async Task<Unit> Handle(SetStockInfoCommand request, CancellationToken cancellationToken)
    {
        var stockSettings = configuration.GetSection("StockSettings").Get<StockSettings>();
        if (stockSettings == null)
            throw new ArgumentNullException(nameof(stockSettings));
        var id = 0;
        foreach (var item in stockSettings.TestStocks)
        {
            var finnResult = await finnHubApi.GetStockInfoAsync(token: finnHubApi.ApiKey, item);
            var stockInfo = finnResult.Result[0];
            stockInfo.Id = id;
            await stockDataRepository.SaveStockDataAsync(stockInfo);
            id++;
        }
        
        return Unit.Value;
    }
}