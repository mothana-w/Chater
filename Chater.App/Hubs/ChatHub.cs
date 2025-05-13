using System.Runtime.CompilerServices;
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
  , IBaseRepository<Room> _roomRepo
  , IBaseRepository<Message> _msgRepo
  , IBaseRepository<RoomMember> _roomMemberRepo
) : Hub
{
  public async Task<HubResult<IEnumerable<MessageResponseDto>>> JoinRoom(string roomName){
    int uid = int.Parse(Context.UserIdentifier!);
    _logger.LogInformation("{user-with-id} joining {room-name}",uid, roomName);

    var room = await _roomRepo.GetSingleAsync(r => r.Name.Equals(roomName));
    if (room is null)
      return new HubResult<IEnumerable<MessageResponseDto>> {
        Code = "ROOM_NOT_FOUND"
        , Message = $"{roomName} is not an existing room"
        , IsSuccess = false
      };
    
    var roomMember =  await _roomMemberRepo.GetSingleAsync(rm => rm.MemeberId.Equals(uid) && rm.RoomId.Equals(room.Id));
    var isJoined = roomMember is not null;
    if (isJoined){
      return new HubResult<IEnumerable<MessageResponseDto>> {
        Code = "ALREADY_JOINED"
        , Message = $"user is already a member in {roomName}"
        , IsSuccess = false
      };
    }

    var newRoomMember = new RoomMember{
      MemeberId = uid,
      RoomId = room.Id,
      JoinedAt = DateTime.UtcNow
    };
    await _roomMemberRepo.AddAsync(newRoomMember);
    var messages = _msgRepo.GetAll(m => m.RoomId == room.Id);

    return new HubResult<IEnumerable<MessageResponseDto>> {
      Code = "JOIN_SUCCESS"
      , IsSuccess = true
      , Data = messages.Select(m => m.MapToDto())
    };
  }
}