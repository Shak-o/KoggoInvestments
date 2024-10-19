using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class GetStocksQueryHandler(IStockDataRepository repository) : IRequestHandler<GetStocksQuery, List<StockDetailViewModel>>
{
    public async Task<List<StockDetailViewModel>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetStockDataAsync();
        result.ForEach(x =>
        {
            x.Price = Math.Round(x.Price, 2);
            x.Diff = Math.Round(x.Diff, 2);
        });

        return result;
    }
}