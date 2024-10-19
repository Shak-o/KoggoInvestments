using KoggoInvestments.Application.Stocks;
using KoggoInvestments.Domain.Stocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoggoInvestments.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StocksController(IMediator mediator) : ControllerBase
{
    [HttpGet("details")]
    public Task<List<StockDetails>> GetDetails() => mediator.Send(new GetStocksQuery());
}