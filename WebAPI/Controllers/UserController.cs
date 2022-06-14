using Application.Users.Commands.Delete;
using Application.Users.Commands.Upsert;
using Application.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UserController : BaseController
{
    //[HttpPost]
    //public async Task<IActionResult> Upsert([FromBody] UpsertUserCommand command)
    //{
    //    var response = await Mediator.Send(command);
    //    return Ok(response);
    //}

    //[HttpDelete]
    //public async Task<IActionResult> Delete([FromBody] DeleteUserCommand command)
    //{
    //    await Mediator.Send(command);
    //    return Ok();
    //}
}
