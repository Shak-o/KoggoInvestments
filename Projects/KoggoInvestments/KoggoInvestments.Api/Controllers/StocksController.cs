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
    public Task<List<StockDetailViewModel>> GetDetails() => mediator.Send(new GetStocksQuery());
    
    // TODO: implement get stock changes by configurtaion -> MVP version get changes by comparison current price vs your requested change check
    [HttpGet("status")]
    public Task<List<CheckStatusResponse>> CheckStatus() => mediator.Send(new CheckStatusCommand());
}