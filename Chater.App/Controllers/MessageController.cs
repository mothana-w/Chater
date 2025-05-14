using System.Security.Claims;
using Chater.App.Services;
using Chater.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chater.App.Controllers;

[ApiController]
[Route("api/messages")]
[Authorize]
public class MessageController(IMessageService _msgService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<MessageResponseDto>>> GetAll(string roomName){
        int uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _msgService.GetAll(uid, roomName);
        if (result.StatusCode.Equals(StatusCodes.Status404NotFound))
            return NotFound(result.ErrorMessage);
        if (result.StatusCode.Equals(StatusCodes.Status403Forbidden))
            return Forbid();
        return Ok(result.Data);
    }
}
