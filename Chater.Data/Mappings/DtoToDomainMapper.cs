using Chater.Data.Model.DTOs;
using Chater.Data.Model.Entities;

namespace Chater.Data.Mappings;

public static class DtoToDomainMapper
{
  public static User MapToUser(this RegistrationRequestDto dto){
    User user = new(){
      Username = dto.Username,
      Email = dto.Email,
      CreatedAt = DateTime.UtcNow,
    };
    return user;
  }
}