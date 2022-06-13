using Application.AdComparasion.Commands.Delete;
using Application.AdComparasion.Commands.Insert;
using Application.AdComparasion.Queries.GetComparasionByUserId;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ComparasionAdController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetUserComparasionAds([FromQuery] GetComparasionByUserIdQuery query)
    {
        var response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InsertComparasionAdCommand command)
    {
        var response = await Mediator.Send(command);
        return response != null ? Ok(response) : BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] DeleteComparasionAdCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
