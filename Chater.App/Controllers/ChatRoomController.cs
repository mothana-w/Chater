using System.Security.Claims;
using Chater.Data.DTOs;
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
}
