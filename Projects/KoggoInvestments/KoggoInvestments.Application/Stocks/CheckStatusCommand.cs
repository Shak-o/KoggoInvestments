using KoggoInvestments.Domain.Stocks;
using MediatR;

namespace KoggoInvestments.Application.Stocks;

public class CheckStatusCommand : IRequest<List<CheckStatusResponse>>
{
    
}