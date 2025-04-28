using Chater.App.Services;
using Chater.Data.Model.DTOs;

namespace Chater.Services;

public interface IAuthService
{
  Task<ServiceResult> RegisterAsync(RegistrationRequestDto dto);
  Task<ServiceResult<string>> LoginAsync(LoginRequestDto dto);
}
