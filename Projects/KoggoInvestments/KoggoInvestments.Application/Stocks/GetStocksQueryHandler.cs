using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class GetStocksQueryHandler(IStockDataRepository repository) : IRequestHandler<GetStocksQuery, List<StockDetails>>
{
    public async Task<List<StockDetails>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetStockDataAsync();
    }
}