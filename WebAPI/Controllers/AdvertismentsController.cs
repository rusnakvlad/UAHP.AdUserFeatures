using Application.Advertisments.Commands.Delete;
using Application.Advertisments.Commands.Upsert;
using Application.Advertisments.Queries.GetAll;
using Application.Advertisments.Queries.GetUserAdvertismentsQuery;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdvertismentsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllAdvertismentsQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("userAdvertisments")]
    public async Task<IActionResult> GetByUserId([FromQuery] GetUserAdvertismentsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertAdCommand command)
    {
        var id = await Mediator.Send(command);
        return Ok(id);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteAdCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }
}
