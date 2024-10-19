using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class CheckStatusCommandHandler (IConfigurationRepository repository, IStockDataRepository stockRepo) : IRequestHandler<CheckStatusCommand, List<CheckStatusResponse>>
{
    public async Task<List<CheckStatusResponse>> Handle(CheckStatusCommand request, CancellationToken cancellationToken)
    {
        var configurations = await repository.GetAllConfigurationsAsync();
        var result = new List<CheckStatusResponse>();
        foreach (var configuration in configurations)
        {
            var stockInfo = await stockRepo.GetStockBarInfoAsync(configuration.StockIdentifier);
            var newRandomStock = new StockBarInfo
            {
                Volume = Random.Shared.Next((int)stockInfo.Volume + Random.Shared.Next(-10,0), (int)stockInfo.Volume + Random.Shared.Next(10)),
                VolumeWeighted = Random.Shared.Next((int)stockInfo.VolumeWeighted + Random.Shared.Next(-10,0), (int)stockInfo.VolumeWeighted + Random.Shared.Next(10)),
                OpeningPrice = Random.Shared.Next((int)stockInfo.OpeningPrice + Random.Shared.Next(-10,0), (int)stockInfo.OpeningPrice + Random.Shared.Next(10)),
                ClosingPrice = Random.Shared.Next((int)stockInfo.ClosingPrice + Random.Shared.Next(-10,0), (int)stockInfo.ClosingPrice + Random.Shared.Next(10)),
                HighestPrice = Random.Shared.Next((int)stockInfo.HighestPrice + Random.Shared.Next(-10,0), (int)stockInfo.HighestPrice + Random.Shared.Next(10)),
                LowesPrice = Random.Shared.Next((int)stockInfo.LowesPrice + Random.Shared.Next(-10,0), (int)stockInfo.LowesPrice + Random.Shared.Next(10)),
                Timestamp = Random.Shared.Next((int)stockInfo.Timestamp + Random.Shared.Next(-10,0), (int)stockInfo.Timestamp + Random.Shared.Next(10)),
                NumberOfTrades = Random.Shared.Next((int)stockInfo.NumberOfTrades + Random.Shared.Next(-10,0), (int)stockInfo.NumberOfTrades + Random.Shared.Next(10))
            };
            var diff = stockInfo.ClosingPrice - newRandomStock.ClosingPrice;
                
            var differenceInPercents = diff / stockInfo.ClosingPrice * 100;
            if (configuration.MaxPercent > 0)
            {
                if (differenceInPercents >= configuration.MaxPercent)
                {
                    result.Add(new CheckStatusResponse()
                    {
                        StockIdentifier = configuration.StockIdentifier,
                        Status = Status.WentUp,
                        PercentValue = configuration.MaxPercent
                    });
                }
                else
                {
                    result.Add(new CheckStatusResponse()
                    {
                        StockIdentifier = configuration.StockIdentifier,
                        Status = Status.None
                    });
                }
            }
            else if (configuration.MinPercent > 0)
            {
               
                if (differenceInPercents >= configuration.MinPercent)
                {
                    result.Add(new CheckStatusResponse()
                    {
                        StockIdentifier = configuration.StockIdentifier,
                        Status = Status.WentDown,
                        PercentValue = configuration.MinPercent
                    });
                }
                else
                {
                    result.Add(new CheckStatusResponse()
                    {
                        StockIdentifier = configuration.StockIdentifier,
                        Status = Status.None
                    });
                }
            }
        }
        // get all configurations
        // foreach
        // get index
        // check rules
        // set result
        // save 
        
        return result;
    }
}