using Chater.App.Services;
using Chater.Data.Model.DTOs;
using Chater.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chater.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _authService, ILogger<AuthController> _logger) : ControllerBase
{
  [HttpPost("register")]
  public async Task<IActionResult> RegisterAsync(RegistrationRequestDto dto){
    ServiceResult result = null!;
    try{ result = await _authService.RegisterAsync(dto);}
    catch {
      _logger.LogError("Unexpected behaviour");
      return BadRequest("Something went wrong, try again later");
    }
    if (result.StatusCode.Equals(StatusCodes.Status400BadRequest))
      return BadRequest(result.ErrorMessage);
    else if (result.StatusCode.Equals(StatusCodes.Status409Conflict))
      return Conflict(result.ErrorMessage);
    else
      return Ok();
  }

  [HttpPost("login")]
  public async Task<ActionResult<string>> LoginAsync(LoginRequestDto dto){
    ServiceResult<string> result = null!;
    try { result = await _authService.LoginAsync(dto); }
    catch {
      _logger.LogError("Unexpected behaviour");
      return BadRequest("Something went wrong, try again later");
    }

    if (result.StatusCode.Equals(StatusCodes.Status400BadRequest))
      return BadRequest(result.ErrorMessage);
    else
      return Ok(result.Data);
    }
}