using Application.Cache.Commands;
using Application.Cache.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CacheController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCacheOjdQuery query)
    {
        var response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Set([FromBody] SetCacheCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }
}
