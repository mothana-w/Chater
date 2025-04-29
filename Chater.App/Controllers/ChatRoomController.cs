using System.Security.Claims;
using Chater.Data.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chater.App.Controllers;

[ApiController]
[Route("api/chat-room")]
[Authorize]
public class ChatRoomController(IChatRoomService _roomSrvc) : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] ChatRoomRequestDto dto){
    int uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    var result = await _roomSrvc.CreateRoom(uid, dto);

    if (result.StatusCode.Equals(StatusCodes.Status400BadRequest))
      return BadRequest(result.ErrorMessage);
    if (result.StatusCode.Equals(StatusCodes.Status409Conflict))
      return BadRequest(result.ErrorMessage);

    return Created();
  }
  [HttpDelete]
  public async Task<IActionResult> Delete(string roomName){
    int uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    var result = await _roomSrvc.DeleteRoom(uid, roomName);

    if (result.StatusCode.Equals(StatusCodes.Status404NotFound))
      return NotFound(result.ErrorMessage);
    if (result.StatusCode.Equals(StatusCodes.Status403Forbidden))
      return Forbid(BearerTokenDefaults.AuthenticationScheme);

    return Ok();
  }
}
