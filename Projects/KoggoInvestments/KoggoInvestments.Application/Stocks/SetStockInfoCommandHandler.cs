using KoggoInvestments.Application.ApiInterfaces;
using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KoggoInvestments.Application.Stocks;

public class SetStockInfoCommandHandler(
    IFinnHubApi finnHubApi,
    IStockDataRepository stockDataRepository,
    IConfiguration configuration,
    IPolygonApi polygonApi,
    ILogger<SetStockInfoCommandHandler> logger)
    : IRequestHandler<SetStockInfoCommand, Unit>
{
    public async Task<Unit> Handle(SetStockInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isEmpty = await stockDataRepository.CheckIfEmptyAsync();
            if (!isEmpty)
                return Unit.Value;
            
            var stockSettings = configuration.GetSection("StockSettings").Get<StockSettings>();
            if (stockSettings == null)
                throw new ArgumentNullException(nameof(stockSettings));
            var id = 0;
            foreach (var item in stockSettings.TestStocks)
            {
                var finnResult = await finnHubApi.GetStockInfoAsync(token: finnHubApi.ApiKey, item);
                var stockInfo = finnResult.Result[0];

                var priceData = await SavePolygonStockData(stockInfo);
                var stockView = new StockDetailViewModel
                {
                    Id = id,
                    Description = stockInfo.Description,
                    DisplaySymbol = stockInfo.DisplaySymbol,
                    Symbol = stockInfo.Symbol,
                    Type = stockInfo.Type,
                    Price = priceData.price,
                    Diff = priceData.diff
                };

                await stockDataRepository.SaveStockDataAsync(stockView);

                id++;
                await Task.Delay(12000);
            }

            return Unit.Value;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private async Task<(decimal price, decimal diff)> SavePolygonStockData(StockDetails stockInfo)
    {
        var ticker = stockInfo.Symbol.ToUpper();
        var apiKey = configuration["PolygonApiKey"];
        var polygonStockData = await polygonApi.GetStockAsync(ticker, apiKey!);
        await stockDataRepository.SavePolygonStockDataAsync(polygonStockData.Results, ticker);
        
        var last = polygonStockData.Results.Last();
        var beforeLast = polygonStockData.Results[^2];
        var lastPrice = last.ClosingPrice;
        var diff = last.ClosingPrice - beforeLast.ClosingPrice;
        
        return (lastPrice, diff);
    }
}