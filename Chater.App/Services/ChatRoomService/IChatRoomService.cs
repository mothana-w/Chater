using Chater.App.Services;
using Chater.Data.DTOs;

public interface IChatRoomService
{
  Task<ServiceResult> CreateRoom(int uid, ChatRoomRequestDto dto);
  Task<ServiceResult> DeleteRoom(int uid, string roomName);

  IEnumerable<ChatRoomResponseDto> GetJoined(int uid);

  Task<ServiceResult<ChatRoomResponseDto>> GetByName(string roomName);

  IEnumerable<ChatRoomResponseDto> GetOwned(int uid);
}