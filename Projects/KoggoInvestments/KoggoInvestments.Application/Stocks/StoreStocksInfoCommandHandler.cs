using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class StoreStocksInfoCommandHandler : IRequestHandler<StoreStocksInfoCommand, Unit>
{
    public Task<Unit> Handle(StoreStocksInfoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}