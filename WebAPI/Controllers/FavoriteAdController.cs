using Application.FavoriteAds.Commands.Delete;
using Application.FavoriteAds.Commands.Insert;
using Application.FavoriteAds.Queries.GetFavoriteAds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoriteAdController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetUserFavoriteAds([FromQuery] GetFavoriteAdsByUserIdQuery query)
    {
        var response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] InsertFavoriteAdCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delte([FromQuery] DeleteFavoriteAdCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
