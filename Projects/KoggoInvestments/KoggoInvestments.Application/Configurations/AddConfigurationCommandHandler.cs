using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Configurations;
using MediatR;

namespace KoggoInvestments.Application.Configurations;

public class AddConfigurationCommandHandler(IConfigurationRepository repository) : IRequestHandler<AddConfigurationCommand, Unit>
{
    public async Task<Unit> Handle(AddConfigurationCommand request, CancellationToken cancellationToken)
    {
        var lastId = await repository.GetMaxIdAsync(request.StockIdentifier);
        var newConfig = new NotificationConfiguration
        {
            Id = lastId + 1,
            StockIdentifier = request.StockIdentifier,
            MaxStockPrice = request.MaxStockPrice,
            MinStockPrice = request.MinStockPrice,
            MaxPercent = request.MaxPercent,
            MinPercent = request.MinPercent
        };

        await repository.AddConfigurationAsync(newConfig);
        return Unit.Value;
    }
}