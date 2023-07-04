using Fanex.BetCount.Business.Services;
using Fanex.MailDelivery.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fanex.BetCount.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ExecuteController : ControllerBase
{
    private readonly MainService mainService;

    public ExecuteController(MainService mainService)
    {
        this.mainService = mainService;
    }

    [HttpGet]
    public JobMessage Get()
    {
        return mainService.Execute();
    }
}