using System.Reflection.Metadata.Ecma335;
using Chater.Data.DTOs;
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
  public static Room MapToRoom(this ChatRoomRequestDto dto){
    return new () {
      Name = dto.Name,
      Description = dto.Description,
      CreatedAt = DateTime.UtcNow
    };
  }
}