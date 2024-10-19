using KoggoInvestments.Application.RepositoryInterfaces;
using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class CheckStatusCommandHandler (IConfigurationRepository repository) : IRequestHandler<CheckStatusCommand, List<CheckStatusResponse>>
{
    public async Task<List<CheckStatusResponse>> Handle(CheckStatusCommand request, CancellationToken cancellationToken)
    {
        var configurations = await repository.GetAllConfigurationsAsync();
        
        foreach (var configuration in configurations)
        {
            
        }
        // get all configurations
        // foreach
        // get index
        // check rules
        // set result
        // save 
        throw new NotImplementedException();
    }
}