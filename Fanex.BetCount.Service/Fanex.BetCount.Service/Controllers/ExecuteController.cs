using Fanex.BetCount.Business.Services;
using Fanex.MailDelivery.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fanex.BetCount.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ExecuteController(MainService mainService) : ControllerBase
{
    [HttpGet]
    public JobMessage Get()
    {
        return mainService.Execute();
    }
}