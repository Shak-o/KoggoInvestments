using KoggoInvestments.Application.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KoggoInvestments.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationsController(IMediator mediator) : ControllerBase
{
    // TODO: implement add configuration
    [HttpPost()]
    public Task AddConfiguration(AddConfigurationCommand command) => mediator.Send(command);
}