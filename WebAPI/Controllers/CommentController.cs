using Application.Comments.Commands.Delete;
using Application.Comments.Queries.GetCommentsByAdId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommentController : BaseController
{
    [HttpGet("GetByAdId")]
    public async Task<IActionResult> GetAllComentsByAdId([FromQuery] GetCommentsByAdIdQuery query)
    {
        var response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> Upsert([FromBody] UpsertCommentCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] DeleteCommentCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
