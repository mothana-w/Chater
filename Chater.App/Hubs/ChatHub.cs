using Chater.Data.DTOs;
using Chater.Data.Mappings;
using Chater.Data.Model.Entities;
using Chater.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chater.Hubs;

[Authorize]
public class ChatHub
(
  ILogger<ChatHub> _logger
  , IBaseRepository<Room> _roomReop
  , IBaseRepository<Message> _msgReop
) : Hub
{
  public async Task<HubResult<IEnumerable<Message>>> JoinRoom(string roomName){
    int uid = int.Parse(Context.UserIdentifier!);
    _logger.LogInformation("{user-with-id} joining {room-name}",uid, roomName);

    var room = await _roomReop.GetSingleAsync(r => r.Name.Equals(roomName));
    if (room is null)
      return new HubResult<IEnumerable<Message>> {
        Code = "ROOM_NOT_FOUND"
        , Message = $"{roomName} is not an existing room"
        , IsSuccess = false
      };
    
    var messages = _msgReop.GetAll(m => m.RoomId == room.Id);

    return new HubResult<IEnumerable<Message>> {
      Code = "JOIN_SUCCESS"
      , IsSuccess = true
      , Data = messages
    };
  }
}