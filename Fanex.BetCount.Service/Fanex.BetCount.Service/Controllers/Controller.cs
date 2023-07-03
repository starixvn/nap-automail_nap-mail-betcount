using Microsoft.AspNetCore.Mvc;

namespace Fanex.BetCount.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    [HttpGet]
    public string Index()
    {
        return "OK";
    }
}