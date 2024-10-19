using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class GetStocksQueryHandler(IStockDataRepository repository) : IRequestHandler<GetStocksQuery, List<StockDetailViewModel>>
{
    public async Task<List<StockDetailViewModel>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetStockDataAsync();
    }
}